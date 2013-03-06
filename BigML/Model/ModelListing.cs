using System;
using System.Threading.Tasks;

namespace BigML
{
    public class ModelListing : Query<Model.Filterable, Model.Orderable, Model>
    {
        public ModelListing(Func<string, Task<Listing<Model>>> client)
            : base(client)
        {
        }
    }
}