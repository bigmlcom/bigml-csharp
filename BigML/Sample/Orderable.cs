using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class Sample
    {
        /// <summary>
        /// Orderable properties for Samples
        /// </summary>
        public class Orderable : Orderable<Sample>
        {
            /// <summary>
            /// The dataset/id that was used to build the sample.
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