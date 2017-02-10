using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BigML
{
    /// <summary>
    /// A topic model is an unsupervised machine learning method for unveiling
    /// all the different topics underlying a collection of documents.
    /// BigML uses Latent Dirichlet allocation (LDA), one of the most popular
    /// probabilistic methods for topic modeling.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/topicmodels">documentation</a>
    /// website.
    /// </summary>
    public partial class TopicModel : Response
    {
        /// <summary>
        /// The number of fields in the topicModel.
        /// </summary>
        public int Columns
        {
            get { return Object.columns;  }
        }

        /// <summary>
        /// The dataset/id that was used to build the topicModel.
        /// </summary>
        public string DataSet
        {
            get { return Object.dataset;  }
        }

        /// <summary>
        /// Whether the dataset is still available or has been deleted.
        /// </summary>
        public bool DataSetStatus
        {
            get { return Object.dataset_status;  }
        }

        /// <summary>
        /// The name of the topicModel as your provided or based on the name of the dataset by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }

        /// <summary>
        /// The current number of predictions that this topicModel has.
        /// </summary>
        public int NumberOfPredictions
        {
            get { return Object.number_of_predictions; }
        }

        /// <summary>
        /// The source/id that was used to build the dataset.
        /// </summary>
        public string Source
        {
            get { return Object.source_id; }
        }

        /// <summary>
        /// Whether the source is still available or has been deleted.
        /// </summary>
        public bool SourceStatus
        {
            get { return Object.source_status; }
        }

        /// <summary>
        /// A description of the status of the topicModel. It includes a code, a message, and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}