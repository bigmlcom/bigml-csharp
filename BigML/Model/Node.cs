using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Linq.Expressions;

namespace BigML
{
    public partial class Model
    {
        /// <summary>
        /// A tree-like recursive structure representing the model.
        /// </summary>
        public class Node
        {
            readonly dynamic _node;
            internal Node(JsonValue json)
            {
                _node = json;
            }

            internal Expression Expression(Dictionary<string, ParameterExpression> parameters)
            {
                var result = System.Linq.Expressions.Expression.Constant(Output) as Expression;
                return Children.Aggregate(result,
                                          (current, child) =>
                                          System.Linq.Expressions.Expression.Condition(
                                              child.Predicate.Expression(parameters), child.Expression(parameters),
                                              current));
            }

            /// <summary>
            /// Array of child Node Objects.
            /// </summary>
            public IEnumerable<Node> Children
            {
                get
                {
                    return (_node.children as JsonValue).Select(child => new Node(child));
                }
            }

            /// <summary>
            /// Number of instances classified by this node.
            /// </summary>
            public int Count
            {
                get { return _node.count; }
            }

            /// <summary>
            /// Distribution of the objective field at this node.
            /// </summary>
            public JsonArray Distribution
            {
                get
                {
                    return _node.distribution;
                }
            }

            /// <summary>
            /// Prediction at this node (number or string)
            /// </summary>
            public string Output
            {
                get { return _node.output; }
            }

            /// <summary>
            /// Predicate structure to make a decision at this node.
            /// </summary>
            public Predicate Predicate
            {
                get { return new Predicate(_node.predicate); }
            }
        }
    }
}