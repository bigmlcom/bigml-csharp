using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class AnomalyScore
    {
        /// <summary>
        /// Orderable properties for Anomaly scores
        /// </summary>
        public class Orderable : Orderable<AnomalyScore>
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