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
                Inputs = new List<string>();
                Range = new List<int>(2);
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
            /// Specifies the number of groups that you do want to be considered when building the cluster.
            /// </summary>
            public int NumberOfCluster
            {
                get;
                set;
            }

            /// <summary>
            /// Specifies the ids of the fields that you want to use as predictors to create the cluster.
            /// TODO use Field?
            /// </summary>
            public ICollection<string> Inputs
            {
                get;
                private set;
            }

            /// <summary>
            /// Specifies the id of the field that you want to predict.
            /// TODO use Field?
            /// </summary>
            public string Objective
            {
                get;
                set;
            }

            /// <summary>
            /// The range of successive instances to build the cluster.
            /// </summary>
            public ICollection<int> Range
            {
                get;
                private set;
            }

            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                if(!string.IsNullOrWhiteSpace(DataSet)) json.dataset = DataSet;
                if(Inputs.Count > 0) json.input_fields = new JsonArray(Inputs.Select(input => (JsonValue)input));
                if (!string.IsNullOrWhiteSpace(Objective)) json.Objective = new JsonArray((JsonValue)Objective);
                if(Range.Count >= 2) json.range = new JsonArray(Range.Take(2).Select(index => (JsonValue)index));

                return json;
            }
        }
    }
}