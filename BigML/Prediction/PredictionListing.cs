using System;
using System.Threading.Tasks;

namespace BigML
{
    public class PredictionListing : Query<Prediction.Filterable, Prediction.Orderable, Prediction>
    {
        public PredictionListing(Func<string, Task<Listing<Prediction>>> client)
            : base(client)
        {
        }
    }
}