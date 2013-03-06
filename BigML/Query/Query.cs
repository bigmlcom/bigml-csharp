using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BigML.Meta;

namespace BigML
{
   
    public abstract class Query<F, O, S> 
        where F : Filterable<S>
        where O : Orderable<S>
        where S : Response, new()
    {
        readonly Func<string, Task<Listing<S>>> _client;

        internal Query(Func<string, Task<Listing<S>>> client)
        {
            _client = client;
        }

        public Filtered<F, O, S> Where(Expression<Func<F, Bool>> predicate)
        {
            return new Filtered<F, O, S>(_client, predicate);
        }

        public Ordered<F, O, S> OrderBy(Expression<Func<O, Meta.Key.Object>> keySelector)
        {
            return new Ordered<F, O, S>(_client, null, keySelector);
        }

        public Ordered<F, O, S> OrderByDescending(Expression<Func<O, Meta.Key.Object>> keySelector)
        {
            return new Ordered<F, O, S>(_client, null, keySelector, true);
        }

    }
}