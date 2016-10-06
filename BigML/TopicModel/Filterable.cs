using BigML.Meta;

namespace BigML
{
    public partial class TopicModel
    {
        /// <summary>
        /// Filterable properties for TopicModel
        /// </summary>
        public class Filterable : Filterable<TopicModel>
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
            /// The current number of predictions that has this topicModel.
            /// </summary>
            public Int NumberOfPredictions
            {
                get { return Object.number_of_predictions; }
            }

            /// <summary>
            /// The source/id that was used to build the topicModel.
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