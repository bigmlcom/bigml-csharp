using System;
using System.Threading.Tasks;

namespace BigML
{
    public class BatchPredictionListing : Query<BatchPrediction.Filterable, BatchPrediction.Orderable, BatchPrediction>
    {
        public BatchPredictionListing(Func<string, Task<Listing<BatchPrediction>>> client)
            : base(client)
        {
        }
    }
}
