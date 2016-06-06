using System;
using System.Threading.Tasks;

namespace BigML
{
    public class ScriptListing : Query<Script.Filterable, Script.Orderable, Script>
    {
        public ScriptListing(Func<string, Task<Listing<Script>>> client): base(client)
        {
        }
    }
}