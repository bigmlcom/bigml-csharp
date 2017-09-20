using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class Forecast
    {
        /// <summary>
        /// Orderable properties for Forecast.
        /// </summary>
        public class Orderable : Orderable<Forecast>
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