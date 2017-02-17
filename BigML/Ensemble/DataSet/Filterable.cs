using BigML.Meta;

namespace BigML
{
    public partial class DataSet
    {
        /// <summary>
        /// Filterable properties for DataSets
        /// </summary>
        public class Filterable : Filterable<DataSet>
        {
            /// <summary>
            /// The number of fields in the dataset.
            /// </summary>
            public Int Columns
            {
                get { return Object.columns; }
            }

            /// <summary>
            /// The current number of models that use this source.
            /// </summary>
            public Int NumberOfModels
            {
                get { return Object.number_of_models; }
            }

            /// <summary>
            /// The current number of predictions that use this source.
            /// </summary>
            public Int NumberOfPredictions
            {
                get { return Object.number_of_predictions; }
            }

            /// <summary>
            /// The total number of rows in the dataset.
            /// </summary>
            public Int Rows
            {
                get { return Object.rows; }
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
