using System;
using System.Threading.Tasks;

namespace BigML
{
    public class BatchTopicDistributionListing : Query<BatchTopicDistribution.Filterable, BatchTopicDistribution.Orderable, BatchTopicDistribution>
    {
        public BatchTopicDistributionListing(Func<string, Task<Listing<BatchTopicDistribution>>> client)
            : base(client)
        {
        }
    }
}