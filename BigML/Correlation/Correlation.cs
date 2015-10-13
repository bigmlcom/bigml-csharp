using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// A correlation resource allows you to compute advanced statistics for
    /// the fields in your dataset by applying various exploratory data
    /// analysis techniques to compare the distributions of the fields in your
    /// dataset against an objective_field.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/developers/correlations">
    /// developers</a> website.
    /// </summary>
    public partial class Correlation : Response
    {

        /// <summary>
        /// The name of the BatchPrediction as your provided or based on the name
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
        /// A description of the status of the correlation. It includes a code, a message,
        /// and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}
