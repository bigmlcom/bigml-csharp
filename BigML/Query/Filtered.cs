using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        readonly Expression<Func<O, Meta.Key.Object>> _keySelector;
        readonly bool _descending;

        readonly Filtered<F, O, S> _child;
        readonly Func<string, Task<Listing<S>>> _client;

        IEnumerable<string> Orderings()
        {
            var orderings = new List<string>();
            if (_child != null) orderings.AddRange(_child.Orderings());
            if (_keySelector != null) orderings.Add(string.Format("order_by={1}{0}", GetProperty(_keySelector.Body), _descending ? "-" : ""));
            return orderings;
        }

        IEnumerable<string> Filters()
        {
            var filters = new List<string>();
            if (_child != null) filters.AddRange(_child.Filters());
            if (_predicate != null) filters.AddRange(GetPredicates(_predicate.Body as BinaryExpression));
            return filters;
        }

        static string GetProperty(Expression left)
        {
            var m = left as MemberExpression;
            var value = Activator.CreateInstance(m.Expression.Type, true);
            var v = (m.Member as PropertyInfo).GetValue(value, null);
            return v.ToString();
        }

        static string NoQuotes<T>(T value)
        {
            return value.ToString().Trim('"');
        }

        static string DateToBigML(System.DateTimeOffset value)
        {
            return ((System.DateTimeOffset)value).ToString("yyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffff");
        }

        static IEnumerable<string> GetPredicates(BinaryExpression expression)
        {
            var filters = new List<string>();

            bool isDate = expression.Right.Type.Name == "DateTimeOffset";
            System.DateTimeOffset leftValue = new System.DateTimeOffset();

            if (isDate)
            {
                var f = Expression.Lambda(expression.Right).Compile();
                leftValue = (System.DateTimeOffset)f.DynamicInvoke();
            }


            switch (expression.NodeType)
            {
                case ExpressionType.NotEqual:
                    filters.Add(string.Format("{0}__={1}", GetProperty(expression.Left),
                                    isDate ? DateToBigML(leftValue) : NoQuotes(expression.Right)));
                    break;
                case ExpressionType.Equal:
                    filters.Add(string.Format("{0}={1}", GetProperty(expression.Left),
                                    isDate ? DateToBigML(leftValue) : NoQuotes(expression.Right)));
                    break;
                case ExpressionType.GreaterThan:
                    filters.Add(string.Format("{0}__gt={1}", GetProperty(expression.Left),
                                    isDate ? DateToBigML(leftValue) : NoQuotes(expression.Right)));
                    break;
                case ExpressionType.LessThan:
                    filters.Add(string.Format("{0}__lt={1}", GetProperty(expression.Left),
                                    isDate ? DateToBigML(leftValue) : NoQuotes(expression.Right)));
                    break;
                case ExpressionType.LessThanOrEqual:
                    filters.Add(string.Format("{0}__lte={1}", GetProperty(expression.Left),
                                    isDate ? DateToBigML(leftValue) : NoQuotes(expression.Right)));
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    filters.Add(string.Format("{0}__gte={1}", GetProperty(expression.Left),
                                    isDate ? DateToBigML(leftValue) : NoQuotes(expression.Right)));
                    break;
                case ExpressionType.AndAlso:
                    filters.AddRange(GetPredicates(expression.Left as BinaryExpression));
                    filters.AddRange(GetPredicates(expression.Right as BinaryExpression));
                    break;
            }
            return filters;
        }


        internal Filtered(Func<string, Task<Listing<S>>> client, Expression<Func<F, Bool>> predicate)
        {
            _client = client;
            _predicate = predicate;
            _child = null;
            _keySelector = null;
            _descending = true;
        }

        public Ordered<F,O,S> OrderBy(Expression<Func<O, Meta.Key.Object>> keySelector)
        {
            return new Ordered<F,O,S>(_client, _predicate, keySelector);
        }

        public Ordered<F,O,S> OrderByDescending(Expression<Func<O, Meta.Key.Object>> keySelector)
        {
            return new Ordered<F,O,S>(_client, _predicate, keySelector, true);
        }

        public TaskAwaiter<Listing<S>> GetAwaiter()
        {
            var filters = Filters();
            var query = string.Join("&", filters);

            return _client(query).GetAwaiter();
        }


        public Task<Listing<S>> InternalTask
        {
            get
            {
                return Do<Listing<S>>(async delegate { return await this; });
            }
        }

        public Listing<S> Result
        {
            get
            {
                return Do<Listing<S>>(async delegate { return await this; }).Result;
            }
        }

        static Task<T> Do<T>(Func<Task<T>> f)
        {
            return f();
        }
    }
}