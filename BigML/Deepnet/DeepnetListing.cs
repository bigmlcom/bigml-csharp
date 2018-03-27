using System;
using System.Threading.Tasks;

namespace BigML
{
    public class DeepnetListing : Query<Deepnet.Filterable, Deepnet.Orderable, Deepnet>
    {
        public DeepnetListing(Func<string, Task<Listing<Deepnet>>> client)
            : base(client)
        {
        }
    }
}
