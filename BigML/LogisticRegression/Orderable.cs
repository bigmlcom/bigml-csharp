using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class LogisticRegression
    {
        /// <summary>
        /// Orderable properties for logistic regressions
        /// </summary>
        public class Orderable : Orderable<LogisticRegression>
        {
            /// <summary>
            /// The number of fields in the dataset.
            /// </summary>
            public Int Columns
            {
                get { return Object.columns; }
            }

            /// <summary>
            /// The dataset/id that was used to build the logistic regression.
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

            /// <summary>
            /// The current number of batch predictions that use this logistic
            /// regression.
            /// </summary>
            public Int NumberOfBatchPredictions
            {
                get { return Object.number_of_evaluations; }
            }

            /// <summary>
            /// The current number of evaluations that use this logistic
            /// regression.
            /// </summary>
            public Int NumberOfEvaluations
            {
                get { return Object.number_of_evaluations; }
            }

            /// <summary>
            /// The current number of predictions that use this logistic
            /// regression.
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
            /// The source/id that was used to build the dataset used to build
            /// the logistic regression.
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