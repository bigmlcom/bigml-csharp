using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class AssociationSet
    {
        public class Arguments : Arguments<AssociationSet>
        {
            public Arguments()
            {
                InputData = new Dictionary<string, object>();
            }

            /// <summary>
            /// A valid association/id.
            /// </summary>
            public string Association
            {
                get;
                set;
            }

            /// <summary>
            /// A group of values in a dictionary: "fieldId": value
            /// </summary>
            public IDictionary<string, object> InputData
            {
                get;
                set;
            }

            public override JObject ToJson()
            {
                dynamic json = base.ToJson();

                if(!string.IsNullOrWhiteSpace(Association)) json.anomaly = Association;
                if (InputData.Count > 0)
                {
                    var input_data = new JObject();
                    foreach (var kv in InputData)
                    {
                        if (kv.Value.GetType().isNumericType())
                        {
                            input_data[kv.Key] = (double)kv.Value;
                        }
                        else
                        {
                            input_data[kv.Key] = (string)kv.Value;
                        }
                    }
                    json.input_data = input_data;
                }
                return json;
            }
        }
    }
}