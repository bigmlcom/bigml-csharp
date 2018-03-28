using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Prediction
    {
        public class Arguments : Arguments<Prediction>
        {
            public Arguments()
            {
                InputData = new Dictionary<string, object>();
            }

            /// <summary>
            /// An object with field's id/value pairs representing the instance you want to create a prediction for.
            /// </summary>
            public IDictionary<string,object> InputData
            {
                get;
                private set;
            }

            /// <summary>
            /// A valid model/id.
            /// </summary>
            public string Model
            {
                get;
                set;
            }

            /// <summary>
            /// A valid ensemble/id.
            /// </summary>
            public string Ensemble
            {
                get;
                set;
            }

            /// <summary>
            /// A valid logisticregression/id.
            /// </summary>
            public string LogisticRegression
            {
                get;
                set;
            }

            public override JObject ToJson()
            {
                dynamic json = base.ToJson();

                if (Ensemble != null)
                {
                    json.ensemble = Ensemble;
                }
                if (LogisticRegression != null)
                {
                    json.logisticregression = LogisticRegression;
                }
                if (Model != null)
                {
                    json.model = Model;
                }

                if (InputData.Count > 0)
                {
                    var input_data = new JObject();
                    JToken jValue;
                    foreach(var kv in InputData)
                    {
                        jValue = JToken.FromObject(kv.Value);
                        if (jValue.Type == JTokenType.Boolean)
                        {
                            input_data[kv.Key] = (bool)kv.Value;
                        }
                        else if (jValue.Type == JTokenType.String)
                        {
                            input_data[kv.Key] = (string)kv.Value;
                        }
                        else if (jValue.Type == JTokenType.Integer)
                        {
                            input_data[kv.Key] = (int)kv.Value;
                        }
                        else if (jValue.Type == JTokenType.Float)
                        {
                            input_data[kv.Key] = (double)kv.Value;
                        }
                        else if (jValue.Type == JTokenType.Object)
                        {
                            input_data[kv.Key] = (JObject)kv.Value;
                        }
                    }
                    json.input_data = input_data;
                }

                return json;
            }
        }
    }
}