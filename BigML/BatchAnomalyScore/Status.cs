using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class BatchAnomalyScore
    {
        /// <summary>
        /// Creating a batch anomaly score is a process that can take just a few
        /// seconds or a few days depending on the size of the dataset used as
        /// input and on the work load of BigML's systems.
        /// The anomaly detector goes through a number of states until its
        /// fully completed.
        /// Through the status field in the resource you can determine when the
        /// batch process has been fully processed.
        /// </summary>
        public class Status : Status<BatchAnomalyScore>
        {
            internal Status(JObject status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the batch score.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}