using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// An OptiML is an automated optimization process for model selection and
    /// parameterization (or hyper-parameterization) to solve classification
    /// and regression problems.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/optimls">documentation</a> website.
    /// </summary>
    public partial class OptiML : Response
    {

        /// <summary>
        /// The name of the OptiML as your provided or based on the name
        /// of the dataset by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }


        /// <summary>
        /// The dataset/id that was used to build the OptiML.
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
        /// A description of the status of the OptiML. It includes a code, a message,
        /// and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}
