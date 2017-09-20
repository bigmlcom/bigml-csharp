using BigML.Meta;

namespace BigML
{
    public partial class TimeSeries
    {
        /// <summary>
        /// Filterable properties for Time Series
        /// </summary>
        public class Filterable : Filterable<TimeSeries>
        {
            /// <summary>
            /// The number of fields in the dataset.
            /// </summary>
            public Int Columns
            {
                get { return Object.columns; }
            }

            /// <summary>
            /// The current number of evaluations that use this time series.
            /// </summary>
            public Int NumberOfEvaluations
            {
                get { return Object.number_of_evaluations; }
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
