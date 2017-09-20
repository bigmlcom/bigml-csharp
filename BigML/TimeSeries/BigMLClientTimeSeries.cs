using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Creates a Time Series using supplied arguments.
        /// </summary>
        public Task<TimeSeries> CreateTimeSeries(TimeSeries.Arguments arguments)
        {
            return Create<TimeSeries>(arguments);
        }

        /// <summary>
        /// Creates a Time Series.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new time series.</param>
        /// <param name="arguments">Specifies the id of the field that you want to predict.</param>
        public Task<TimeSeries> CreateTimeSeries(DataSet dataset, string name = null,
                                            TimeSeries.Arguments arguments = null)
        {
            arguments = arguments ?? new TimeSeries.Arguments();
            if (!string.IsNullOrWhiteSpace(name))
                arguments.Name = name;
            arguments.DataSet = dataset.Resource;
            return Create<TimeSeries>(arguments);
        }

        /// <summary>
        /// List all the time series
        /// </summary>
        public Query<TimeSeries.Filterable, TimeSeries.Orderable, TimeSeries> ListTimeSeries()
        {
            return new TimeSeriesListing(List<TimeSeries>);
        }
    }
}