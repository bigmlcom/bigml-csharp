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

            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                if(!string.IsNullOrWhiteSpace(DataSet)) json.dataset = DataSet;
                json.k = NumberOfCluster;
                return json;
            }
        }
    }
}