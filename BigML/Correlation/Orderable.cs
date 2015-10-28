using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class Correlation
    {
        /// <summary>
        /// Orderable properties for correlations
        /// </summary>
        public class Orderable : Orderable<Correlation>
        {
            /// <summary>
            /// The dataset/id that was used to build the correlation.
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