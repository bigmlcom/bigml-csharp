using System.Json;

namespace BigML
{
    public partial class BatchCentroid
    {
        /// <summary>
        /// Creating an batch centroid is a process that can take just a few
        /// seconds or a few days depending on the size of the dataset used as
        /// input and on the work load of BigML's systems.
        /// The batch prediction goes through a number of states until its
        /// fully completed.
        /// Through the status field in the batchcentroid you can determine
        /// when the batch process has been fully completed.
        /// </summary>
        public class Status : Status<BatchCentroid>
        {
            internal Status(JsonValue status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the batch centroid.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}