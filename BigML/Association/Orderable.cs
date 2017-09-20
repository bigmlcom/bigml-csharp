using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class Association
    {
        /// <summary>
        /// Orderable properties for Associations
        /// </summary>
        public class Orderable : Orderable<Association>
        {
            /// <summary>
            /// The number of fields in the dataset.
            /// </summary>
            public Int Columns
            {
                get { return Object.columns; }
            }

            /// <summary>
            /// The dataset/id that was used to build the association.
            /// </summary>
            public String Dataset
            {
                get { return Object.dataaset; }
            }

            /// <summary>
            /// Whether the dataset is still available or has been deleted.
            /// </summary>
            public Bool DatasetStatus
            {
                get { return Object.dataset_status; }
            }

            /// <summary>
            /// The current number of association sets that use this
            /// association.
            /// </summary>
            public Int NumberOfAssociationSets
            {
                get { return Object.number_of_associationsets; }
            }

            /// <summary>
            /// The number of instances used to build the association.
            /// </summary>
            public Int Rows
            {
                get { return Object.rows; }
            }

            /// <summary>
            /// The source/id that was used to build the dataset used to build
            /// the association
            /// </summary>
            public String Source
            {
                get { return Object.source; }
            }

            /// <summary>
            /// Whether the source is still available or has been deleted.
            /// </summary>
            public Bool SourceStatus
            {
                get { return Object.source_status; }
            }
        }
    }
}