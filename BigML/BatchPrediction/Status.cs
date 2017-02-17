using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class BatchPrediction
    {
        /// <summary>
        /// Creating a batch prediction is a process that can take just a few
        /// seconds or a few days depending on the size of the dataset used as
        /// input and on the work load of BigML's systems.
        /// The batch prediction goes through a number of states until its
        /// fully completed.
        /// Through the status field in the batchprediction you can determine
        /// when the batch prediction has been fully completed.
        /// </summary>
        public class Status : Status<BatchPrediction>
        {
            internal Status(JObject status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the Batch Prediction.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}