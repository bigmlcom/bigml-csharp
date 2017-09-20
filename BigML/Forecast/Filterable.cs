using BigML.Meta;

namespace BigML
{
    public partial class Forecast
    {
        /// <summary>
        /// Filterable properties for Forecast.
        /// </summary>
        public abstract class Filterable : Filterable<Forecast>
        {
            /// <summary>
            /// The dataset/id that was used to build the timeseries.
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
            /// The timeseries/id that was used to build the prediction.
            /// </summary>
            public String TimeSeries
            {
                get { return Object.timeseries; }
            }

            /// <summary>
            /// Whether the timeseries is still available or has been deleted.
            /// </summary>
            public Bool TimeSeriesStatus
            {
                get { return Object.timeseries_status; }
            }

            /// <summary>
            /// The objective field of the prediction.
            /// </summary>
            //[Name("objective_fields")]
            //public String ObjectiveField { get { return Object.timeseries_status; } }

            /// <summary>
            /// The objective field of the prediction.
            /// </summary>
            //[Name("prediction")]
            //public String Forecast { get { return Object.timeseries_status; } }

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