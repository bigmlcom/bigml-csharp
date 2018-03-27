using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a deepnet using supplied arguments.
        /// </summary>
        public Task<Deepnet> CreateDeepnet(Deepnet.Arguments arguments)
        {
            return Create<Deepnet>(arguments);
        }

        /// <summary>
        /// Create a Deepnet.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new deepnet.</param>
        /// <param name="arguments">Other extra parameters.</param>
        public Task<Deepnet> CreateDeepnet(DataSet dataset, string name = null,
                                            Deepnet.Arguments arguments = null)
        {
            arguments = arguments ?? new Deepnet.Arguments();
            if (!string.IsNullOrWhiteSpace(name))
                arguments.Name = name;
            arguments.DataSet = dataset.Resource;
            return Create<Deepnet>(arguments);
        }

        /// <summary>
        /// List all the Deepnet
        /// </summary>
        public Query<Deepnet.Filterable, Deepnet.Orderable, Deepnet> ListDeepnet()
        {
            return new DeepnetListing(List<Deepnet>);
        }
    }
}