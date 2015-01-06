using System;
using System.Threading.Tasks;

namespace BigML
{
    public class CentroidListing : Query<Centroid.Filterable, Centroid.Orderable, Centroid>
    {
        public CentroidListing(Func<string, Task<Listing<Centroid>>> client)
            : base(client)
        {
        }
    }
}
