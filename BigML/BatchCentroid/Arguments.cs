using System.Collections.Generic;
using System.Json;
using System.Linq;

namespace BigML
{
    public partial class BatchCentroid
    {
        public class Arguments : Arguments<BatchCentroid>
        {
            public Arguments()
            {

            }

            /// <summary>
            /// A valid cluster/id.
            /// </summary>
            public string Cluster
            {
                get;
                set;
            }

            /// <summary>
            /// A valid dataset/id.
            /// </summary>
            public string DataSet
            {
                get;
                set;
            }

            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                if (!string.IsNullOrWhiteSpace(Cluster)) json.cluster = Cluster;
                if (!string.IsNullOrWhiteSpace(DataSet)) json.dataset = DataSet;
                return json;
            }
        }
    }
}