using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class Deepnet
    {
        /// <summary>
        /// Orderable properties for deepnet
        /// </summary>
        public class Orderable : Orderable<Deepnet>
        {
            /// <summary>
            /// The dataset/id that was used to build the deepnet.
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