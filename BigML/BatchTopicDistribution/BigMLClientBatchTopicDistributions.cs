using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a batch topic distribution using supplied arguments.
        /// </summary>
        public Task<BatchTopicDistribution> CreateBatchTopicDistribution(BatchTopicDistribution.Arguments arguments)
        {
            return Create<BatchTopicDistribution>(arguments);
        }

        /// <summary>
        /// Create a BatchTopicDistribution.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new batch topic distributions. </param>
        /// <param name="arguments">Specifies the data that you want to predict.</param>
        public Task<BatchTopicDistribution> CreateBatchTopicDistribution(DataSet dataset, string name = null,
                                            BatchTopicDistribution.Arguments arguments = null)
        {
            arguments = arguments ?? new BatchTopicDistribution.Arguments();
            if (!string.IsNullOrWhiteSpace(name))
                arguments.Name = name;
            arguments.DataSet = dataset.Resource;
            return Create<BatchTopicDistribution>(arguments);
        }

        /// <summary>
        /// List all the Batch TopicDistributions
        /// </summary>
        public Query<BatchTopicDistribution.Filterable, BatchTopicDistribution.Orderable, BatchTopicDistribution> ListBatchTopicDistributions()
        {
            return new BatchTopicDistributionListing(List<BatchTopicDistribution>);
        }
    }
}