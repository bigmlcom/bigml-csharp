using BigML.Meta;

namespace BigML
{
    public partial class AssociationSet
    {
        /// <summary>
        /// Filterable properties for Association sets
        /// </summary>
        public class Filterable : Filterable<AssociationSet>
        {
            /// <summary>
            /// The dataset/id that was used to build the association.
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
