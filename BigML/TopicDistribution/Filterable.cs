using BigML.Meta;

namespace BigML
{
    public partial class TopicDistribution
    {
        /// <summary>
        /// Filterable properties for TopicDistribution.
        /// </summary>
        public abstract class Filterable : Filterable<TopicDistribution>
        {
            /// <summary>
            /// The dataset/id that was used to build the topicModel.
            /// </summary>
            public String DataSet
            {
                get { return Object.dataset; }
            }

            /// <summary>
            /// Whether the dataset is still available or has been deleted.
            /// </summary>
            public Bool DataStatus
            {
                get { return Object.dataset_status; }
            }

            /// <summary>
            /// The topicModel/id that was used to build the prediction.
            /// </summary>
            public String TopicModel
            {
                get { return Object.topicModel; }
            }

            /// <summary>
            /// Whether the topicModel is still available or has been deleted.
            /// </summary>
            public Bool TopicModelStatus
            {
                get { return Object.topicModel_status; }
            }

            /// <summary>
            /// The source/id that was used to build the dataset.
            /// </summary>
            public String Source
            {
                get { return Object.source; }
            }

            /// <summary>
            /// Whether the source is still available or has been deleted.
            /// </summary>
            public Bool SourceStatus
            {
                get { return Object.source_status; }
            }
        }
    }
}