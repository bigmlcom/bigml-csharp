using System.Json;

namespace BigML
{
    public partial class BatchTopicDistribution
    {
        /// <summary>
        /// Creating a batch topic distribution is a process that can take just a few
        /// seconds or a few days depending on the size of the dataset used as
        /// input and on the work load of BigML's systems.
        /// The batch topic distribution goes through a number of states until its
        /// fully completed.
        /// Through the status field in the batchtopicdistribution you can determine
        /// when the batch topic distribution has been fully completed.
        /// </summary>
        public class Status : Status<BatchTopicDistribution>
        {
            internal Status(JsonValue status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the Batch TopicDistribution.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}