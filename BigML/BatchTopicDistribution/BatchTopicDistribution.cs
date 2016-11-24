using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// A batch topic distribution provides an easy way to compute a topic
    /// distribution for each instance in a dataset in only one request.
    /// To create a new batch topic distribution you need a topicmodel/id
    /// and a dataset/id.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/batchtopicdistributions">
    /// documentation</a> website.
    /// </summary>
    public partial class BatchTopicDistribution : Response
    {

        /// <summary>
        /// The name of the BatchTopicDistribution as your provided or based on the name
        /// of the dataset and the model/ensemble by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }


        /// <summary>
        /// The dataset/id that was used to build the dataset.
        /// </summary>
        public string DataSet
        {
            get { return Object.dataset; }
        }



        /// <summary>
        /// Whether the dataset is still available or has been deleted.
        /// </summary>
        public bool DataSetStatus
        {
            get { return Object.dataset_status; }
        }


        /// <summary>
        /// A description of the status of the model. It includes a code, a message,
        /// and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }

    }
}
