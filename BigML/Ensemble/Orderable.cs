using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class Ensemble
    {
        /// <summary>
        /// Orderable properties for Ensembles
        /// </summary>
        public class Orderable : Orderable<Ensemble>
        {

            /// <summary>
            /// The current number of predictions that use this ensemble.
            /// </summary>
            public Int NumberOfPredictions
            {
                get { return Object.number_of_predictions; }
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