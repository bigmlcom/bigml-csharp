using System;
using System.Threading.Tasks;

namespace BigML
{
    public class ExecutionListing : Query<Execution.Filterable, Execution.Orderable, Execution>
    {
        public ExecutionListing(Func<string, Task<Listing<Execution>>> client): base(client)
        {
        }
    }
}