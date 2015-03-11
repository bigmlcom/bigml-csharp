using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a BatchCentroid using supplied arguments.
        /// </summary>
        public Task<BatchCentroid> CreateBatchCentroid(BatchCentroid.Arguments arguments)
        {
            return Create<BatchCentroid>(arguments);
        }

        /// <summary>
        /// Create a Batch Centroid
        /// </summary>
        /// <param name="cluster">A Cluster instance</param>
        /// <param name="name">The name you want to give to the new batch centroid. </param>
        /// <param name="arguments">Specifies other arguments for the batch centroid.</param>
        public Task<BatchCentroid> CreateBatchCentroid(Cluster cluster, string name = null,
                                            BatchCentroid.Arguments arguments = null)
        {
            arguments = arguments ?? new BatchCentroid.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) 
                arguments.Name = name;
            arguments.Cluster = cluster.Resource;
            return Create<BatchCentroid>(arguments);
        }

        /// <summary>
        /// List all the batch centroids
        /// </summary>
        public Query<BatchCentroid.Filterable, BatchCentroid.Orderable, BatchCentroid> ListBatchCentroids()
        {
            return new BatchCentroidListing(List<BatchCentroid>);
        }
    }
}