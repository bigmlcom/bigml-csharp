using System;
using System.Threading.Tasks;

namespace BigML
{
    public class AnomalyListing : Query<Anomaly.Filterable, Anomaly.Orderable, Anomaly>
    {
        public AnomalyListing(Func<string, Task<Listing<Anomaly>>> client)
            : base(client)
        {
        }
    }
}
