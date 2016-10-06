using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a new topicDistribution.
        /// </summary>
        public Task<TopicDistribution> CreateTopicDistribution(TopicDistribution.Arguments arguments)
        {
            return Create(arguments);
        }

        /// <summary>
        /// Create a new topicDistribution.
        /// </summary>
        /// <param name="topicModel">A valid TopicModel</param>
        /// <param name="name">The name you want to give to the new topicDistribution. </param>
        public Task<TopicDistribution> CreateTopicDistribution(TopicModel topicModel, string name = null, TopicDistribution.Arguments arguments = null)
        {
            arguments = arguments ?? new TopicDistribution.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) arguments.Name = name;
            arguments.TopicModel = topicModel.Resource;
            return Create(arguments);
        }

        public Query<TopicDistribution.Filterable, TopicDistribution.Orderable, TopicDistribution> ListTopicDistributions()
        {
            return new TopicDistributionListing(List<TopicDistribution>);
        }
    }
}