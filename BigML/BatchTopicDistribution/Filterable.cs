using BigML.Meta;

namespace BigML
{
    public partial class BatchTopicDistribution
    {
        /// <summary>
        /// Filterable properties for BatchTopicDistributions
        /// </summary>
        public class Filterable : Filterable<BatchTopicDistribution>
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
