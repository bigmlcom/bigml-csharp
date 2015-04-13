using System.Collections.Generic;
using System.Json;
using System.Linq;

namespace BigML
{
    public partial class Ensemble
    {
        public class Arguments : Arguments<Ensemble>
        {
            public Arguments()
            {
                ExcludedFields = new List<string>();
            }

            /// <summary>
            /// A valid dataset/id.
            /// </summary>
            public string DataSet
            {
                get;
                set;
            }

            /// <summary>
            /// A list of strings that specifies the fields that won't be
            /// included in the ensemble
            /// </summary>
            public List<string> ExcludedFields
            {
                get;
                set;
            }

            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                if(!string.IsNullOrWhiteSpace(DataSet)) json.dataset = DataSet;
                if (ExcludedFields.Count > 0)
                {
                    var excluded_fields = new JsonArray();
                    foreach (var excludedField in ExcludedFields)
                    {
                        excluded_fields.Add((JsonValue)excludedField);
                    }
                    json.excluded_fields = excluded_fields;
                }
                return json;
            }
        }
    }
}