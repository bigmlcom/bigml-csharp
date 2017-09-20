using System;
using System.Threading.Tasks;

namespace BigML
{
    public class ForecastListing : Query<Forecast.Filterable, Forecast.Orderable, Forecast>
    {
        public ForecastListing(Func<string, Task<Listing<Forecast>>> client)
            : base(client)
        {
        }
    }
}