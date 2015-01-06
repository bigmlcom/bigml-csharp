using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a model using supplied arguments.
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
        /// <param name="inputFields">Specifies the ids of the fields that you want to use as important to create the cluster. </param>
        /// <param name="objectiveField">Specifies the id of the field that you want to predict.</param>
        public Task<Cluster> CreateCluster(DataSet dataset, string name = null, int k = 8,
                                            Cluster.Arguments arguments = null)
        {
            arguments = arguments ?? new Cluster.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) arguments.Name = name;
            if (k != null && k > 0)
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