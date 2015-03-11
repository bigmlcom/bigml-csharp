using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class BatchCentroid
    {
        /// <summary>
        /// Orderable properties for BatchCentroids
        /// </summary>
        public class Orderable : Orderable<BatchCentroid>
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
            public Bool AnomalyStatus
            {
                get { return Object.anomaly_status; }
            }
        }
    }
}