using System.Collections.Generic;
using System.Json;
using System.Linq;

namespace BigML
{
    public partial class Cluster
    {
        public class Arguments : Arguments<Cluster>
        {
            public Arguments()
            {
                NumberOfCluster = 8;
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
            /// Specifies the number of groups that you do want to be considered
            /// when building the cluster.
            /// </summary>
            public int NumberOfCluster
            {
                get;
                set;
            }

            /// <summary>
            /// A list of strings that specifies the fields that won't be
            /// included in the cluster
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
                json.k = NumberOfCluster;
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