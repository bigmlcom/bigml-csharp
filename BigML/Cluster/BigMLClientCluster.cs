using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a cluster using supplied arguments.
        /// </summary>
        public Task<Cluster> CreateCluster(Cluster.Arguments arguments)
        {
            return Create<Cluster>(arguments);
        }

        /// <summary>
        /// Create a cluster.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new cluster. </param>
        /// <param name="k">Specifies the number of groups you want to create as parts of the cluster. </param>
        /// <param name="arguments">An object with more cluster parameters.</param>
        public Task<Cluster> CreateCluster(DataSet dataset, string name = null, int k = 8,
                                            Cluster.Arguments arguments = null)
        {
            arguments = arguments ?? new Cluster.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) arguments.Name = name;
            if (k > 0)
            {
                arguments.NumberOfCluster = k;
            }
            arguments.DataSet = dataset.Resource;
            return Create<Cluster>(arguments);
        }

        /// <summary>
        /// List all clusters
        /// </summary>
        public Query<Cluster.Filterable, Cluster.Orderable, Cluster> ListClusters()
        {
            return new ClusterListing(List<Cluster>);
        }
    }
}