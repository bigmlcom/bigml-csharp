using System.Collections.Generic;
using System.Json;
using System.Linq;

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
            /// A valid anomaly/id.
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

            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                if(!string.IsNullOrWhiteSpace(Association)) json.anomaly = Association;
                if (InputData.Count > 0)
                {
                    var input_data = new JsonObject();
                    foreach (var kv in InputData)
                    {
                        JsonPrimitive value;
                        JsonPrimitive.TryCreate(kv.Value, out value);
                        input_data[kv.Key] = value;
                    }
                    json.input_data = input_data;
                }
                return json;
            }
        }
    }
}