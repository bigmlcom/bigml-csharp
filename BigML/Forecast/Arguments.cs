using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Forecast
    {
        public class Arguments : Arguments<Forecast>
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
            /// A valid timeseries/id.
            /// </summary>
            public string Timeseries
            {
                get;
                set;
            }


            public override JObject ToJson()
            {
                dynamic json = base.ToJson();

                if (Timeseries != null)
                {
                    json.timeseries = Timeseries;
                }

                if (InputData.Count > 0)
                {
                    var input_data = new JObject();
                    JToken jValue;
                    foreach(var kv in InputData)
                    {
                        jValue = (JToken)kv.Value;
                        if (jValue.Type == JTokenType.Boolean)
                        {
                            input_data[kv.Key] = (bool)kv.Value;
                        }
                        else if (jValue.Type == JTokenType.String)
                        {
                            input_data[kv.Key] = (string)kv.Value;
                        }
                        else
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