using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// A sample provides fast-access to the raw data of a dataset on an
    /// on-demand basis.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/samples">documentation</a>
    /// website.
    /// </summary>
    public partial class Sample : Response
    {

        /// <summary>
        /// The name of the sample as your provided or based on the name
        /// of the dataset by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }


        /// <summary>
        /// The dataset/id that was used to build the sample.
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
        /// A description of the status of the sample. It includes a code, a message,
        /// and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}
