using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a OptiML using supplied arguments.
        /// </summary>
        public Task<OptiML> CreateOptiML(OptiML.Arguments arguments)
        {
            return Create<OptiML>(arguments);
        }

        /// <summary>
        /// Create a OptiML.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new OptiML. </param>
        /// <param name="arguments">The other parameters you want to specify (description, tags, etc.)</param>
        public Task<OptiML> CreateOptiML(DataSet dataset, string name = null,
                                         OptiML.Arguments arguments = null)
        {
            arguments = arguments ?? new OptiML.Arguments();
            if (!string.IsNullOrWhiteSpace(name))
                arguments.Name = name;
            arguments.DataSet = dataset.Resource;
            return Create<OptiML>(arguments);
        }

        /// <summary>
        /// List all the OptiMLs
        /// </summary>
        public Query<OptiML.Filterable, OptiML.Orderable, OptiML> ListOptiMLs()
        {
            return new OptimlListing(List<OptiML>);
        }
    }
}