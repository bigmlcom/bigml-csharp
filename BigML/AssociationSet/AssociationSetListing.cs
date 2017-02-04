using System;
using System.Threading.Tasks;

namespace BigML
{
    public class AssociationSetListing : Query<AssociationSet.Filterable, AssociationSet.Orderable, AssociationSet>
    {
        public AssociationSetListing(Func<string, Task<Listing<AssociationSet>>> client)
            : base(client)
        {
        }
    }
}
