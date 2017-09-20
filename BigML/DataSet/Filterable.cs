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
            /// The current number of models that use this dataset.
            /// </summary>
            public Int NumberOfModels
            {
                get { return Object.number_of_models; }
            }

            /// <summary>
            /// The current number of ensembles that use this dataset.
            /// </summary>
            public Int NumberOfEnsembles
            {
                get { return Object.number_of_ensembles; }
            }

            /// <summary>
            /// The current number of logistic regresssions that use this dataset.
            /// </summary>
            public int NumberOfLogisticRegressions
            {
                get { return Object.number_of_logisticregressions; }
            }

            /// <summary>
            /// The current number of time series that use this dataset.
            /// </summary>
            public int NumberOfTimeSeries
            {
                get { return Object.number_of_timeseries; }
            }

            /// <summary>
            /// The current number of clusters that use this dataset.
            /// </summary>
            public int NumberOfClusters
            {
                get { return Object.number_of_clusters; }
            }

            /// <summary>
            /// The current number of anomaly detectors (anomalies) that use this dataset.
            /// </summary>
            public int NumberOfAnomalies
            {
                get { return Object.number_of_anomalies; }
            }

            /// <summary>
            /// The current number of association discoveries (associations) that use this dataset.
            /// </summary>
            public int NumberOfAssociations
            {
                get { return Object.number_of_associations; }
            }

            /// <summary>
            /// The current number of topic models that use this dataset.
            /// </summary>
            public int NumberOfTopicModels
            {
                get { return Object.number_of_topicmodels; }
            }

            /// <summary>
            /// The current number of predictions that use this dataset.
            /// </summary>
            public int NumberOfPredictions
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
