using BigML.Meta;

namespace BigML
{
    public partial class BatchCentroid
    {
        /// <summary>
        /// Filterable properties for BatchCentroid
        /// </summary>
        public class Filterable : Filterable<BatchCentroid>
        {
            /// <summary>
            /// The anomaly/id that was used to build the BatchCentroid.
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
