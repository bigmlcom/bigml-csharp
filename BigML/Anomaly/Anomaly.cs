using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// An anomaly detector is a predictive model that can help identify the
    /// instances within a dataset that do not conform to a regular pattern.
    /// It can be useful for tasks like data cleansing, identifying unusual
    /// instances, or, given a new data point, deciding whether a model is
    /// competent to make a prediction or not.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/anomalies">documentation</a>
    /// website.
    /// </summary>
    public partial class Anomaly : Response
    {

        /// <summary>
        /// The name of the anomaly detection as your provided or based on the name 
        /// of the dataset by default.
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
        /// A description of the status of the anomaly detector.
        /// It includes a code, a message, and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}
