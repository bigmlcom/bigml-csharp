using System;
using System.Threading.Tasks;

namespace BigML
{
    public class ConfigurationListing : Query<Configuration.Filterable, Configuration.Orderable, Configuration>
    {
        public ConfigurationListing(Func<string, Task<Listing<Configuration>>> client)
            : base(client)
        {
        }
    }
}
