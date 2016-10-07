using System;
using System.Collections.Generic;
using System.Json;
using System.Text.RegularExpressions;

namespace BigML
{
    public partial class Model
    {
        public class LocalModel
        {
            readonly JsonValue _jsonObject;
            readonly Dictionary<string, DataSet.Field> _fields;

            readonly Dictionary<string, bool> _caseSensitive;
            readonly Dictionary<string, Dictionary<string, string>> _termForms;

            private Dictionary<string, string> nameToIdDict = new Dictionary<string, string>();
            private Node rootNode;

            private System.Globalization.CultureInfo provider = new System.Globalization.CultureInfo("en-US");

            private string[] modelFieldsNames;
            private Dictionary<string, bool> fieldAllowEmpty = new Dictionary<string, bool>();

            public Dictionary<string, DataSet.Field> Fields
            {
                get { return _fields; }
            }


            private Dictionary<string, string> getFieldTermAnalysis(DataSet.Field currentField)
            {
                Dictionary<string, string> options = new Dictionary<string, string>();
                DataSet.Field.Summary.Text textSummary;
                string fieldId, regex;
                int i, totalOptions;

                fieldId = currentField.Id;
                textSummary = (DataSet.Field.Summary.Text)currentField.FieldSummary;

                //term options per field
                regex = "";
                foreach (KeyValuePair<string, string[]> termOptions in textSummary.TermForms)
                {
                    regex = "(\b|_)" + termOptions.Key + "(\b|_)";
                    totalOptions = termOptions.Value.Length;
                    for (i = 0; i < totalOptions; i++)
                    {
                        regex += "|(\b|_)" + termOptions.Value[i] + "(\b|_)";
                    }
                    options.Add(termOptions.Key, regex);
                }

                return options;
            }

            internal LocalModel(JsonValue jsonObject, Dictionary<string, DataSet.Field> fields)
            {
                Dictionary<string, Dictionary<string, string>> termForms = new Dictionary<string, Dictionary<string, string>>();
                Dictionary<string, string> options;
                Dictionary<string, bool> caseSensitive = new Dictionary<string, bool>();
                string fieldId;
                DataSet.Field currentField;

                _jsonObject = jsonObject;

                // parse or fill the Fields information
                if (fields != null) {
                    _fields = fields;

                    // populate nameToIdDict from 'fields' param
                    foreach (var kv in fields)
                    {
                        fieldId = kv.Key;
                        nameToIdDict.Add(_fields[fieldId].Name, fieldId);

                        //text analysis initialization
                        options = new Dictionary<string, string>();

                        if (_fields[fieldId].OpType == OpType.Text)
                        {
                            currentField = fields[kv.Key];

                            //case sensitive per field
                            caseSensitive.Add(fieldId, (bool)currentField.TermAnalysis["case_sensitive"]);

                            //text analysis initialization
                            options = getFieldTermAnalysis(currentField);
                        }
                        fieldAllowEmpty[fieldId] = _fields[fieldId].OpType.Equals(OpType.Text) || _fields[fieldId].OpType.Equals(OpType.Items);
                        termForms[fieldId] = options;
                    }
                }
                else {
                    _fields = new Dictionary<string, DataSet.Field>();

                    // process each 'field' properties and 
                    // populate nameToIdDict from 'fields' param
                    foreach (var kv in _jsonObject["fields"])
                    {
                        fieldId = kv.Key;
                        _fields[fieldId] = new DataSet.Field(kv.Value);
                        nameToIdDict.Add(_fields[fieldId].Name, fieldId);

                        //text analysis initialization
                        options = new Dictionary<string, string>();

                        if (_fields[fieldId].OpType == OpType.Text)
                        {
                            currentField = fields[kv.Key];

                            //case sensitive per field
                            caseSensitive.Add(fieldId, (bool)currentField.TermAnalysis["case_sensitive"]);

                            options = getFieldTermAnalysis(currentField);
                        }

                        fieldAllowEmpty[fieldId] = _fields[fieldId].OpType.Equals(OpType.Text) || _fields[fieldId].OpType.Equals(OpType.Items);
                        termForms[fieldId] = options;
                    }
                }

                // fill the array with all the fields' names
                modelFieldsNames = new string[nameToIdDict.Keys.Count];
                nameToIdDict.Keys.CopyTo(modelFieldsNames, 0);

                _caseSensitive = caseSensitive;
                _termForms = termForms;
            }


            private DataSet.Field getFieldInfo(string fieldId)
            {
                if (_fields.ContainsKey(fieldId)) {
                    return _fields[fieldId];
                }
                return null;
            }


            public DataSet.Field getFieldByName(string fieldName)
            {
                return getFieldInfo(nameToIdDict[fieldName]);
            }


            public DataSet.Field getFieldById(string fieldId)
            {
                return getFieldInfo(fieldId);
            }


            private int termMatches(string text, string fieldLabel, string term)
            {
                RegexOptions flags = RegexOptions.IgnoreCase;

                Dictionary<string, string> wordsMatch = _termForms[fieldLabel];
                MatchCollection matches;
                if (wordsMatch.ContainsKey(term))
                {
                    if (_caseSensitive.ContainsKey(fieldLabel))
                    {
                        matches = Regex.Matches(text, term);
                    }
                    else
                    {
                        matches = Regex.Matches(text, term, flags);
                    }
                    return matches.Count;
                }
                return 0;
            }


            private int countWords(string inData, string predicateValue)
            {
                int inDataLength = inData.Length;
                int originalLenght = predicateValue.Length;
                string stringClean = predicateValue.Replace(inData, "");
                int newLength = stringClean.Length;

                return (originalLenght - newLength) / inDataLength;
            }


            private Node predictNode(Node currentNode, Dictionary<string, dynamic> inputData)
            {
                bool missingField;
                string fieldId;
                dynamic predicateValue;
                dynamic inDataValue;

                foreach (Node children in currentNode.Children)
                {
                    fieldId = children.Predicate.Field;
                    missingField = !inputData.ContainsKey(fieldId);

                    if ((missingField) && (children.Predicate.MissingOperator))
                    {
                        predictNode(children, inputData);
                    }
                    else
                    {
                        inDataValue = inputData[fieldId];
                        predicateValue = children.Predicate.Value;

                        //is a text field or items
                        if (children.Predicate.HasTerm)
                        {
                            inDataValue = termMatches(inDataValue, fieldId, children.Predicate.Term);
                        }

                        switch (children.Predicate.Operator)
                        {
                            case "<":
                            case "<*":
                                if (inDataValue < predicateValue)
                                {
                                    return predictNode(children, inputData);
                                }
                                break;
                            case "<=":
                            case "<=*":
                                if (inDataValue <= predicateValue)
                                {
                                    return predictNode(children, inputData);
                                }
                                break;
                            case ">":
                            case ">*":
                                if (inDataValue > predicateValue)
                                {
                                    return predictNode(children, inputData);
                                }
                                break;
                            case ">=*":
                            case ">=":
                                if (inDataValue >= predicateValue)
                                {
                                    return predictNode(children, inputData);
                                }
                                break;
                            case "=*":
                            case "==*":
                            case "=":
                            case "==":
                                if (inDataValue == predicateValue)
                                {
                                    return predictNode(children, inputData);
                                }
                                break;
                            case "!=*":
                            case "!=":
                            case "<>*":
                            case "<>":
                                if (inDataValue != predicateValue)
                                {
                                    return predictNode(children, inputData);
                                }
                                break;

                            default:
                                throw new System.Exception(children.Predicate.Operator + " is not recognized");
                        }
                    }
                }
                return currentNode;
            }


            string fieldId;
            Dictionary<string, dynamic> inputDataByFieldId;
            /// <summary>
            /// Clean input data introduced for a prediction removing fields 
            /// without valid value and populates the result's Dictionary
            /// by fieldID
            /// </summary>
            /// <param name="inputData">Original input data</param>
            /// <returns>A map by field ID and clean values</returns>
            public Dictionary<string, dynamic> prepareInputData(Dictionary<string, dynamic> inputData, bool byName=true)
            {
                inputDataByFieldId = new Dictionary<string, dynamic>();
                foreach (string key in inputData.Keys)
                {
                    if (nameToIdDict.ContainsKey(key) && byName)
                    {
                        fieldId = nameToIdDict[key];
                        inputDataByFieldId[fieldId] = inputData[key];
                    }
                    else
                    {
                        fieldId = key;
                        inputDataByFieldId[key] = inputData[key];
                    }

                    // remove empty numbers or categoricals
                    if (!fieldAllowEmpty[fieldId] &&
                            inputDataByFieldId[fieldId].ToString() == "")
                    {
                        inputDataByFieldId.Remove(fieldId);
                    }
                }
                return inputDataByFieldId;
            }

            
            /// <summary>
            /// Generates a prediction based on this Model
            /// </summary>
            /// <param name="inputData">Map with the ID or name of each field and its value</param>
            /// <param name="byName">Flag for the inputData key: 
            ///                         * true if fieldName is the key,
            ///                         * false if the key is the field ID</param>
            /// <param name="missing_strategy">Which strategy should use the prediction</param>
            /// <returns>The Node where Model's navigation has stopped</returns>
            public Node predict(Dictionary<string, dynamic> inputData, bool byName=true, int missing_strategy=0)
            {
                if (byName)
                {
                    inputData = prepareInputData(inputData, byName);
                }

                // if Model was not processed before => creates root Node
                if (rootNode == null) { 
                    rootNode = new Node(this._jsonObject["root"]);
                }

                return predictNode(rootNode, inputData);
            }
        }
    }
}

