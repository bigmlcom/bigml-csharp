using System;
using System.Threading.Tasks;

namespace BigML
{
    public class EnsembleListing : Query<Ensemble.Filterable, Ensemble.Orderable, Ensemble>
    {
        public EnsembleListing(Func<string, Task<Listing<Ensemble>>> client)
            : base(client)
        {
        }
    }
}
