using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// Deepnets are an optimized version of Deep Neural Networks, a class of
    /// machine-learned models inspired by the neural circuitry of the human
    /// brain.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/deepnets">
    /// documentation</a> website.
    /// </summary>
    public partial class Deepnet : Response
    {

        /// <summary>
        /// The name of the Deepnet as your provided or based on the name
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
        /// A description of the status of the deepnet.
        /// It includes a code, a message, and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}
