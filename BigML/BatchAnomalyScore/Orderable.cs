using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class BatchAnomalyScore
    {
        /// <summary>
        /// Orderable properties for Batch Scores
        /// </summary>
        public class Orderable : Orderable<BatchAnomalyScore>
        {
            /// <summary>
            /// The anomaly/id that was used to build the dataset.
            /// </summary>
            public String Anomaly
            {
                get { return Object.anomaly; }
            }

            /// <summary>
            /// Whether the anomaly is still available or has been deleted.
            /// </summary>
            public Bool SourceStatus
            {
                get { return Object.anomaly_status; }
            }
        }
    }
}