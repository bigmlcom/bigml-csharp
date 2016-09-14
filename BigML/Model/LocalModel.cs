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

            Dictionary<string, string> nameToIdDict = new Dictionary<string, string>();

            internal LocalModel(JsonValue jsonObject, JsonValue fields)
            {
                Dictionary<string, bool> caseSensitive = new Dictionary<string, bool>();
                Dictionary<string, Dictionary<string, string>> termForms = new Dictionary<string, Dictionary<string, string>>();

                _jsonObject = jsonObject;
                _fields = new Dictionary<string, DataSet.Field>();

                foreach (var kv in fields)
                {
                    string fieldId = kv.Key;
                    _fields[fieldId] = new DataSet.Field(kv.Value);
                    nameToIdDict.Add(_fields[fieldId].Name, fieldId);

                    //text analysis initialization
                    Dictionary<string, string> options = new Dictionary<string, string>();

                    if (_fields[fieldId].OpType == OpType.Text)
                    {
                        DataSet.Field currentField = _fields[kv.Key];
                        DataSet.Field.Summary.Text textSummary = (DataSet.Field.Summary.Text)currentField.FieldSummary;

                        //case sensitive per field
                        caseSensitive.Add(fieldId, (bool) currentField.TermAnalysis["case_sensitive"]);

                        //term options per field
                        string regex = "";
                        foreach (KeyValuePair<string, string[]> termOptions in textSummary.TermForms)
                        {
                            regex = "(\b|_)" + termOptions.Key + "(\b|_)";
                            int totalOptions = termOptions.Value.Length;
                            for (int i = 0; i < totalOptions; i++)
                            {
                                regex += "|(\b|_)" + termOptions.Value[i] + "(\b|_)";
                            }
                            options.Add(termOptions.Key, regex);
                        }
                    }
                    termForms[fieldId] = options;
                }
                _caseSensitive = caseSensitive;
                _termForms = termForms;
            }


            private System.Globalization.NumberStyles style = System.Globalization.NumberStyles.AllowDecimalPoint;
            private System.Globalization.CultureInfo provider = new System.Globalization.CultureInfo("en-US");


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
                var stringClean = predicateValue.Replace(inData, "");
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
                    predicateValue = children.Predicate.Value;

                    missingField = !inputData.ContainsKey(fieldId);
                    if (!missingField)
                    {
                        inDataValue = inputData[fieldId];
                    }
                    else
                    {
                        inDataValue = null;
                    }

                    if (missingField)
                    {
                        if (children.Predicate.MissingOperator)
                        {
                            currentNode = children;
                        }
                        else
                        {
                            return currentNode;
                        }
                    }

                    if (children.Predicate.Term != null && children.Predicate.Term != "")
                    {
                        //is a text field
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
                return currentNode;
            }
        
            public Node predict(Dictionary<string, dynamic> inputData, bool byName = true, int missing_strategy = 0)
            {
                IList<Prediction> outputs = new List<Prediction>();
 
                Dictionary<string, dynamic> dataById = new Dictionary <string, dynamic>();
                string[] fieldsNames = new string[nameToIdDict.Keys.Count];
                nameToIdDict.Keys.CopyTo(fieldsNames, 0);
                foreach (string key in inputData.Keys)
                {
                    if (Array.IndexOf(fieldsNames, key) > -1) {
                        dataById[nameToIdDict[key]] = inputData[key];
                    }
                    else {
                        dataById[key] = inputData[key];
                    }
                }

                var root = this._jsonObject["root"];
                Node rootNode = new Node(root);

                return predictNode(rootNode, dataById);
            }
        }
    }
}

