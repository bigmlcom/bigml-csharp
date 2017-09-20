using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class Cluster
    {
        /// <summary>
        /// Orderable properties for Clusters
        /// </summary>
        public class Orderable : Orderable<Cluster>
        {
            /// <summary>
            /// The number of fields in the cluster.
            /// </summary>
            public Int Columns
            {
                get { return Object.columns; }
            }

            /// <summary>
            /// The dataset/id that was used to build cluster.
            /// </summary>
            public String Dataset
            {
                get { return Object.dataset; }
            }

            /// <summary>
            /// Whether the dataset is still available or has been deleted.
            /// </summary>
            public Bool DatasetStatus
            {
                get { return Object.dataset_status; }
            }

            /// <summary>
            /// The number of clusters.
            /// </summary>
            public Bool K
            {
                get { return Object.k; }
            }

            /// <summary>
            /// The current number of batch centroids that use this cluster.
            /// </summary>
            public Int NumberOfBatchCentroids
            {
                get { return Object.number_of_batchcentroids; }
            }

            /// <summary>
            /// The current number of centroids that use this cluster.
            /// </summary>
            public Int NumberOfCentroids
            {
                get { return Object.number_of_centroids; }
            }

            /// <summary>
            /// The total number of instances used to build the cluster.
            /// </summary>
            public Int Rows
            {
                get { return Object.rows; }
            }

            /// <summary>
            /// The source/id that was used to build the dataset used to build
            /// the cluster.
            /// </summary>
            public String Source
            {
                get { return Object.source; }
            }

            /// <summary>
            /// Whether the source is still available or has been deleted.
            /// </summary>
            public Bool SourceStatus
            {
                get { return Object.source_status; }
            }
        }
    }
}