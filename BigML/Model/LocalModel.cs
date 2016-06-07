using System.Collections.Generic;
using System.Json;


namespace BigML
{
    public partial class Model
    {
        public class LocalModel
        {
            readonly JsonValue _jsonObject;
            readonly Dictionary<string, DataSet.Field> _modelFields;

            internal LocalModel(JsonValue jsonObject, JsonValue modelFields)
            {
                _jsonObject = jsonObject;
                _modelFields = new Dictionary<string, DataSet.Field>();
                foreach (var kv in modelFields)
                {
                    _modelFields[kv.Key] = new DataSet.Field(kv.Value);
                }
            }


            private System.Globalization.NumberStyles style = System.Globalization.NumberStyles.AllowDecimalPoint;
            private System.Globalization.CultureInfo provider = new System.Globalization.CultureInfo("en-US");

            private Node predictNode(Node currentNode, Dictionary<string, dynamic> inputData)
            {
                bool isMissingSplit = false;
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

                    isMissingSplit = false;
                    if (children.Predicate.Operator.Contains("*"))
                    {
                        // missing operator
                        isMissingSplit = true;

                    }
                    if (missingField)
                    {
                        if (isMissingSplit)
                        {
                            currentNode = children;
                        }
                        else
                        {
                            return currentNode;
                        }
                    }

                    predicateValue = double.Parse(predicateValue, style, provider);

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
                            throw new System.Exception();
                    }
                }
                return currentNode;
            }
        
            public Node predict(Dictionary<string, dynamic> inputData, bool byName = true, int missing_strategy = 0)
            {
                IList<Prediction> outputs = new List<Prediction>();
 
                Dictionary<string, dynamic> dataById = new Dictionary <string, dynamic>();

                var root = this._jsonObject["root"];
                Node rootNode = new Node(root);

                return predictNode(rootNode, inputData);
            }
        }
    }
}

