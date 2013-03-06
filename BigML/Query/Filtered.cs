using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BigML.Meta;

namespace BigML
{
    public class Filtered<F,O,S> 
        where F: Filterable<S> 
        where O: Orderable<S> 
        where S: Response, new()
    {
        readonly Expression<Func<F, Bool>> _predicate;
        readonly Func<string, Task<Listing<S>>> _client;

        internal Filtered(Func<string, Task<Listing<S>>> client, Expression<Func<F, Bool>> predicate)
        {
            _client = client;
            _predicate = predicate;
        }

        public Ordered<F,O,S> OrderBy(Expression<Func<O, Meta.Key.Object>> keySelector)
        {
            return new Ordered<F,O,S>(_client, _predicate, keySelector);
        }

        public Ordered<F,O,S> OrderByDescending(Expression<Func<O, Meta.Key.Object>> keySelector)
        {
            return new Ordered<F,O,S>(_client, _predicate, keySelector, true);
        }
    }
}