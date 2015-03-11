using BigML.Meta;

namespace BigML
{
    public partial class BatchPrediction
    {
        /// <summary>
        /// Filterable properties for BatchPredictions
        /// </summary>
        public class Filterable : Filterable<BatchPrediction>
        {
            /// <summary>
            /// The dataset/id that was used to build the dataset.
            /// </summary>
            public String DataSet
            {
                get { return Object.dataset; }
            }

            /// <summary>
            /// Whether the dataset is still available or has been deleted.
            /// </summary>
            public Bool DataSetStatus
            {
                get { return Object.dataset_status; }
            }
        }
    }
}
