using System;
using System.Threading.Tasks;

namespace BigML
{
    public class AssociationListing : Query<Association.Filterable, Association.Orderable, Association>
    {
        public AssociationListing(Func<string, Task<Listing<Association>>> client)
            : base(client)
        {
        }
    }
}
