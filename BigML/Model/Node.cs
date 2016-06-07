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

            internal Expression ConfidVal(Dictionary<string, ParameterExpression> parameters)
            {
                var confidence = System.Linq.Expressions.Expression.Constant(Confidence) as Expression;

                return Children.Aggregate(confidence,
                                          (current, child) =>
                                          System.Linq.Expressions.Expression.Condition(
                                              child.Predicate.Expression(parameters), child.ConfidVal(parameters),
                                              current));
            }

            internal Expression ComplexResult(Dictionary<string, ParameterExpression> parameters)
            {
                ResultNode rn = new ResultNode();
                rn.Output = Output;
                rn.Confidence = Confidence;
                rn.Count = Count;

                var result = System.Linq.Expressions.Expression.Constant(rn) as Expression;

                return Children.Aggregate(result,
                                          (current, child) =>
                                          System.Linq.Expressions.Expression.Condition(
                                              child.Predicate.Expression(parameters), child.ComplexResult(parameters),
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
            /// The confidence of the output with more weight in this node.
            /// </summary>
            public float Confidence
            {
                get { return _node.confidence; }
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
            public JsonValue Distribution
            {
                get
                {
                    return _node["objective_summary"]["categories"];
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

            public Dictionary<object, object> toDictionary()
            {
                Dictionary<object, object>  dictionaryResult = new Dictionary<object, object>();
                dictionaryResult.Add("prediction", this.Output);
                dictionaryResult.Add("confidence", this.Confidence);
                dictionaryResult.Add("count", this.Count);
                dictionaryResult.Add("distribution", this.Distribution);

                return dictionaryResult;
            }

            public override string ToString()
            {
                return Output + " " + Confidence.ToString() +
                        " Distribution:" + _node.objective_summary.ToString();
            }
        }
    }
}