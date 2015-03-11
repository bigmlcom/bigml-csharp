using System;
using System.Threading.Tasks;

namespace BigML
{
    public class SampleListing : Query<Sample.Filterable, Sample.Orderable, Sample>
    {
        public SampleListing(Func<string, Task<Listing<Sample>>> client)
            : base(client)
        {
        }
    }
}
