using System.Collections.Generic;
using System.Json;
using System.Linq;

namespace BigML
{
    public partial class BatchTopicDistribution
    {
        public class Arguments : Arguments<BatchTopicDistribution>
        {
            public Arguments()
            {

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
            /// A valid topicmodel/id.
            /// </summary>
            public string TopicModel
            {
                get;
                set;
            }

            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                if (!string.IsNullOrWhiteSpace(DataSet)) json.dataset = DataSet;
                if (!string.IsNullOrWhiteSpace(DataSet)) json.topicmodel = TopicModel;
                return json;
            }
        }
    }
}