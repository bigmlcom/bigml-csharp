using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class OptiML
    {
        /// <summary>
        /// Orderable properties for OptiMLs
        /// </summary>
        public class Orderable : Orderable<OptiML>
        {
            /// <summary>
            /// The dataset/id that was used to build the OptiML.
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