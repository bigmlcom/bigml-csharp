using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class Model
    {
        /// <summary>
        /// Filterable properties for Model
        /// </summary>
        public class Orderable : Orderable<Model>
        {
            /// <summary>
            /// The number of fields in the dataset.
            /// </summary>
            public Int Columns
            {
                get { return Object.columns; }
            }

            /// <summary>
            /// The dataset/id that was used to build the model.
            /// </summary>
            public String DataSet
            {
                get { return Object.dataset; }
            }

            /// <summary>
            /// Whether the dataset is still available or has been deleted.
            /// </summary>
            public Bool DataStatus
            {
                get { return Object.dataset_status; }
            }

            /// <summary>
            /// The holdout size used to build the model.
            /// </summary>
            public Double HouldOut
            {
                get { return Object.holdout; }
            }

            /// <summary>
            /// The total number of fields in the dataset used to build the model.
            /// </summary>
            public Int MaxColumns
            {
                get { return Object.max_columns; }
            }

            /// <summary>
            /// The total number of rows in the dataset used to build the model.
            /// </summary>
            public Int MaxRows
            {
                get { return Object.max_rows; }
            }

            /// <summary>
            /// The current number of predictions that use this model.
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