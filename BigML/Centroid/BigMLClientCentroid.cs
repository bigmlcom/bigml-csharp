using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a model using supplied arguments.
        /// </summary>
        public Task<Centroid> CreateCentroid(Centroid.Arguments arguments)
        {
            return Create<Centroid>(arguments);
        }

        /// <summary>
        /// Create a cluster.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new cluster. </param>
        /// <param name="arguments">Specifies the id of the field that you want to predict.</param>
        public Task<Centroid> CreateCentroid(DataSet dataset, string name = null, Centroid.Arguments arguments = null)
        {
            arguments = arguments ?? new Centroid.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) arguments.Name = name;
            arguments.DataSet = dataset.Resource;
            return Create<Centroid>(arguments);
        }

        /// <summary>
        /// List all centroids
        /// </summary>
        public Query<Centroid.Filterable, Centroid.Orderable, Centroid> ListCentroid()
        {
            return new CentroidListing(List<Centroid>);
        }
    }
}