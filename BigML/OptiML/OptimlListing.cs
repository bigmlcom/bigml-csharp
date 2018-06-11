using System;
using System.Threading.Tasks;

namespace BigML
{
    public class OptimlListing : Query<OptiML.Filterable, OptiML.Orderable, OptiML>
    {
        public OptimlListing(Func<string, Task<Listing<OptiML>>> client)
            : base(client)
        {
        }
    }
}
