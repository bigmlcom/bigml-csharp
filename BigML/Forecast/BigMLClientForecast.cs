using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a new forecast.
        /// </summary>
        public Task<Forecast> CreateForecast(Forecast.Arguments arguments)
        {
            return Create<Forecast>(arguments);
        }

        /// <summary>
        /// Create a new forecast.
        /// </summary>
        /// <param name="timeseries">A valid TimeSeries</param>
        /// <param name="name">The name you want to give to the new forecast. </param>
        public Task<Forecast> CreateForecast(TimeSeries timeseries, string name = null, Forecast.Arguments arguments = null)
        {
            arguments = arguments ?? new Forecast.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) arguments.Name = name;
            arguments.TimeSeries = timeseries.Resource;
            return Create<Forecast>(arguments);
        }

        public Query<Forecast.Filterable, Forecast.Orderable, Forecast> ListForecasts()
        {
            return new ForecastListing(List<Forecast>);
        }
    }
}