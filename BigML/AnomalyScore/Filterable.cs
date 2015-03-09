using BigML.Meta;

namespace BigML
{
    public partial class AnomalyScore
    {
        /// <summary>
        /// Filterable properties for Anomaly scores
        /// </summary>
        public class Filterable : Filterable<AnomalyScore>
        {
            /// <summary>
            /// The dataset/id that was used to build the anomaly detector.
            /// </summary>
            public String Dataset
            {
                get { return Object.dataset; }
            }

            /// <summary>
            /// Whether the dataset is still available or has been deleted.
            /// </summary>
            public Bool DatasetStatus
            {
                get { return Object.dataset_status; }
            }
        }
    }
}
