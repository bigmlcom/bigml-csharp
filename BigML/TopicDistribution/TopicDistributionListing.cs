using System;
using System.Threading.Tasks;

namespace BigML
{
    public class TopicDistributionListing : Query<TopicDistribution.Filterable, TopicDistribution.Orderable, TopicDistribution>
    {
        public TopicDistributionListing(Func<string, Task<Listing<TopicDistribution>>> client)
            : base(client)
        {
        }
    }
}