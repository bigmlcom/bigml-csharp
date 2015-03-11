using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    public partial class BatchCentroid : Response
    {


        /// <summary>
        /// The name of the batch centroid as your provided or based on the name 
        /// of the cluster by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }


        /// <summary>
        /// The cluster/id that was used to build the dataset.
        /// </summary>
        public string Cluster
        {
            get { return Object.cluster; }
        }



        /// <summary>
        /// Whether the cluster is still available or has been deleted.
        /// </summary>
        public bool ClusterStatus
        {
            get { return Object.dataset_status; }
        }


        /// <summary>
        /// A description of the status of the BatchCentroid. 
        /// It includes a code, a message, and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}
