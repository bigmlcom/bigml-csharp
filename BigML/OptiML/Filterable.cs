using BigML.Meta;

namespace BigML
{
    public partial class OptiML
    {
        /// <summary>
        /// Filterable properties for OptiML
        /// </summary>
        public class Filterable : Filterable<OptiML>
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
