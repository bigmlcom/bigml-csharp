using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class TimeSeries
    {
        /// <summary>
        /// Orderable properties for time series
        /// </summary>
        public class Orderable : Orderable<TimeSeries>
        {
            /// <summary>
            /// The number of fields in the dataset.
            /// </summary>
            public Int Columns
            {
                get { return Object.columns; }
            }

            /// <summary>
            /// The current number of forecasts that use this time series.
            /// </summary>
            public Int NumberOfForecasts
            {
                get { return Object.number_of_forecasts; }
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