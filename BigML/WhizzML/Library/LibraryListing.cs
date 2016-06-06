using System;
using System.Threading.Tasks;

namespace BigML
{
    public class LibraryListing : Query<Library.Filterable, Library.Orderable, Library>
    {
        public LibraryListing(Func<string, Task<Listing<Library>>> client): base(client)
        {
        }
    }
}