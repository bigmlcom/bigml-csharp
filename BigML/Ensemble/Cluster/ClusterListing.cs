using System;
using System.Threading.Tasks;

namespace BigML
{
    public class ClusterListing : Query<Cluster.Filterable, Cluster.Orderable, Cluster>
    {
        public ClusterListing(Func<string, Task<Listing<Cluster>>> client)
            : base(client)
        {
        }
    }
}
