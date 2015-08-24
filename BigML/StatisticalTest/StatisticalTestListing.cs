using System;
using System.Threading.Tasks;

namespace BigML
{
    public class StatisticalTestListing : Query<StatisticalTest.Filterable, StatisticalTest.Orderable, StatisticalTest>
    {
        public StatisticalTestListing(Func<string, Task<Listing<StatisticalTest>>> client)
            : base(client)
        {
        }
    }
}
