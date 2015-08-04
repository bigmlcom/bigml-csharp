using BigML.Meta;

namespace BigML
{
    public partial class BatchAnomalyScore
    {
        /// <summary>
        /// Filterable properties for Batch scores
        /// </summary>
        public class Filterable : Filterable<BatchAnomalyScore>
        {
            /// <summary>
            /// The anomaly/id that was used to build the batch score.
            /// </summary>
            public String Anomaly
            {
                get { return Object.anomaly; }
            }

            /// <summary>
            /// Whether the anomaly is still available or has been deleted.
            /// </summary>
            public Bool AnomalyStatus
            {
                get { return Object.anomaly_status; }
            }
        }
    }
}
