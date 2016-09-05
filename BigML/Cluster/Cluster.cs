using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// A cluster is a set of groups (i.e., clusters) of instances of a dataset
    /// that have been automatically classified together according to a distance
    /// measure computed using the fields of the dataset. Each group is
    /// represented by a centroid or center that is computed using the mean for
    /// each numeric field and the mode for each categorical field.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/clusters">documentation</a>
    /// website.
    /// </summary>
    public partial class Cluster : Response
    {

        /// <summary>
        /// The name of the cluster as your provided or based on the name of the dataset by default.
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
        /// A description of the status of the cluster. It includes a code, a message, and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}
