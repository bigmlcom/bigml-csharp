using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// A batch centroid provides an easy way to compute a centroid for each
    /// instance in a dataset in only one request. To create a new batch
    /// centroid you need a cluster/id and a dataset/id.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/batch_centroids">
    /// documentation</a> website.
    /// </summary>
    public partial class BatchCentroid : Response
    {

        /// <summary>
        /// The name of the batch centroid as your provided or based on the name
        /// of the cluster and dataset by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }


        /// <summary>
        /// The cluster/id that was used to build the batch centroid.
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
        /// The dataset/id that was used to build the batch centroid.
        /// </summary>
        public string DataSet
        {
            get { return Object.dataset; }
        }



        /// <summary>
        /// Whether the dataset is still available or has been deleted.
        /// </summary>
        public bool DatasetStatus
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
