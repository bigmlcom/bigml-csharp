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
            /// The source/id that was used to build the dataset.
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