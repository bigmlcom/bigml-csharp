using System;
using System.Threading.Tasks;

namespace BigML
{
    public class DataSetListing : Query<DataSet.Filterable, DataSet.Orderable, DataSet>
    {
        public DataSetListing(Func<string, Task<Listing<DataSet>>> client)
            : base(client)
        {
        }
    }
}