using System;
using System.Threading.Tasks;

namespace BigML
{
    public class SourceListing : Query<Source.Filterable, Source.Orderable, Source>
    {
        public SourceListing(Func<string, Task<Listing<Source>>> client)
            : base(client)
        {

        }
    }
}