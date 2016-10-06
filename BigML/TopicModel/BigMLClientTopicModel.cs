using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a topicModel using supplied arguments.
        /// </summary>
        public Task<TopicModel> CreateTopicModel(TopicModel.Arguments arguments)
        {
            return Create<TopicModel>(arguments);
        }


        /// <summary>
        /// List all topicModels
        /// </summary>
        public Query<TopicModel.Filterable, TopicModel.Orderable, TopicModel> ListTopicModels()
        {
            return new TopicModelListing(List<TopicModel>);
        }
    }
}