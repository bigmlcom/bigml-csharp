using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a configuration using supplied arguments.
        /// </summary>
        public Task<Configuration> CreateConfiguration(Configuration.Arguments arguments)
        {
            return Create<Configuration>(arguments);
        }


        /// <summary>
        /// List all configuration
        /// </summary>
        public Query<Configuration.Filterable, Configuration.Orderable, Configuration> ListConfigurations()
        {
            return new ConfigurationListing(List<Configuration>);
        }
    }
}