using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// A logistic regression is a supervised machine learning method for
    /// solving classification problems. The probability of the objective being
    /// a particular class is modeled as the value of a logistic function,
    /// whose argument is a linear combination of feature values.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/logisticregressions">
    /// documentation</a> website.
    /// </summary>
    public partial class LogisticRegression : Response
    {

        /// <summary>
        /// The name of the logistic regression as your provided or based on the name
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
        /// A description of the status of the logistic regression.
        /// It includes a code, a message, and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}
