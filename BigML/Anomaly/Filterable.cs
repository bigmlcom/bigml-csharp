using BigML.Meta;

namespace BigML
{
    public partial class Anomaly
    {
        /// <summary>
        /// Filterable properties for Anomalies
        /// </summary>
        public class Filterable : Filterable<Anomaly>
        {
            /// <summary>
            /// The number of fields in the anomaly detector.
            /// </summary>
            public Int Columns
            {
                get { return Object.columns; }
            }

            /// <summary>
            /// The dataset/id that was used to build anomaly detector.
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
            /// The current number of anomaly scores that use this anomaly.
            /// </summary>
            public Int NumberOfAnomalyScores
            {
                get { return Object.number_of_anomalyscores; }
            }

            /// <summary>
            /// The current number of batch anomaly scores that use this
            /// anomaly detector.
            /// </summary>
            public Int NumberOfBatchAnomalyScores
            {
                get { return Object.number_of_batchanomalyscores; }
            }

            /// <summary>
            /// The total number of rows used to build the anomaly detector.
            /// </summary>
            public Int Rows
            {
                get { return Object.rows; }
            }

            /// <summary>
            /// The source/id that was used to build the dataset used to create
            /// the anomaly detector.
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

            /// <summary>
            /// The number of top anomalies returned after scoring each row
            /// in the training dataset.
            /// </summary>
            public Int TopN
            {
                get { return Object.top_n; }
            }

        }
    }
}
