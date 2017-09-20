using System;
using System.Threading.Tasks;

namespace BigML
{
    public class TimeSeriesListing : Query<TimeSeries.Filterable, TimeSeries.Orderable, TimeSeries>
    {
        public TimeSeriesListing(Func<string, Task<Listing<TimeSeries>>> client)
            : base(client)
        {
        }
    }
}
