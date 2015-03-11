using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class BatchCentroid
    {
        /// <summary>
        /// Orderable properties for BatchCentroids
        /// </summary>
        public class Orderable : Orderable<BatchCentroid>
        {
            /// <summary>
            /// The cluster/id that was used to build the BatchCentroid.
            /// </summary>
            public String Cluster
            {
                get { return Object.cluster; }
            }

            /// <summary>
            /// Whether the cluster is still available or has been deleted.
            /// </summary>
            public Bool ClusterStatus
            {
                get { return Object.cluster_status; }
            }

            /// <summary>
            /// The dataset/id that was used to build the BatchCentroid.
            /// </summary>
            public String DataSet
            {
                get { return Object.dataset; }
            }

            /// <summary>
            /// Whether the dataset is still available or has been deleted.
            /// </summary>
            public Bool DataSetStatus
            {
                get { return Object.dataset_status; }
            }

            /// <summary>
            /// The total number of rows in the batch centroid.
            /// </summary>
            public Int Rows
            {
                get { return Object.rows; }
            }
        }
    }
}