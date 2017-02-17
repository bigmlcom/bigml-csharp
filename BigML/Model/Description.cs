using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Model
    {
        public new class Description
        {
            readonly dynamic _description;
            readonly IEnumerable<string> _inputFields;

            internal Description(JValue description, IEnumerable<string> inputfields)
            {
                _description = description;
                _inputFields = inputfields;
            }
            
            /// <summary>
            /// Compile Model description to Expression
            /// </summary>
            public LambdaExpression Expression()
            {
                var parameters = new Dictionary<string, ParameterExpression>();
                foreach (var field in Fields)
                {
                    if (!_inputFields.Contains(field.Key)) continue;
                    var type = field.Value.OpType.TypeOf();
                    var parameter = System.Linq.Expressions.Expression.Parameter(type, field.Value.Name);
                    parameters[field.Key] = parameter;
                }

                var body = Root.Expression(parameters);
                var lambda = System.Linq.Expressions.Expression.Lambda(body, parameters.Values);
                return lambda;
            }


            /// <summary>
            /// Compile Model description to Expression, focused on confidence
            /// </summary>
            public LambdaExpression ConfidenceValue()
            {
                var parameters = new Dictionary<string, ParameterExpression>();
                foreach (var field in Fields)
                {
                    if (!_inputFields.Contains(field.Key)) continue;
                    var type = field.Value.OpType.TypeOf();
                    var parameter = System.Linq.Expressions.Expression.Parameter(type, field.Value.Name);
                    parameters[field.Key] = parameter;
                }

                var body = Root.ConfidVal(parameters);
                var lambda = System.Linq.Expressions.Expression.Lambda(body, parameters.Values);
                return lambda;
            }


            /// <summary>
            /// Compile Model description to Expression using ResultNode objects
            /// </summary>
            public LambdaExpression ComplexResult()
            {
                var parameters = new Dictionary<string, ParameterExpression>();
                foreach (var field in Fields)
                {
                    if (!_inputFields.Contains(field.Key)) continue;
                    var type = field.Value.OpType.TypeOf();
                    var parameter = System.Linq.Expressions.Expression.Parameter(type, field.Value.Name);
                    parameters[field.Key] = parameter;
                }

                var body = Root.ComplexResult(parameters);
                var lambda = System.Linq.Expressions.Expression.Lambda(body, parameters.Values);
                return lambda;
            }

            /// <summary>
            /// A dictionary with an entry per field in the model. 
            /// Each entry includes the column number in original source, the name of the field, the type of the field, and the summary.
            /// </summary>
            public IDictionary<string, DataSet.Field> Fields
            {
                get
                {
                    var dictionary = new Dictionary<string, DataSet.Field>();
                    foreach (var kv in _description.fields)
                    {
                        dictionary[kv.Key] = new DataSet.Field(kv.Value);
                    }
                    return dictionary;
                }
            }

            /// <summary>
            /// The type of model. So far you will only get "stree".
            /// </summary>
            public string Kind
            {
                get { return _description.kind; }
            }

            /// <summary>
            /// The dataset's locale.
            /// </summary>
            public string Locale
            {
                get { return _description.locale; }
            }

            /// <summary>
            /// Strategy followed by the model in case it founds a missing value. 
            /// So far you will only get "last_prediction".
            /// </summary>
            public string MissingStrategy
            {
                get { return _description.missing_strategy; }
            }

            /// <summary>
            /// A Node Object, a tree-like recursive structure representing the model.
            /// </summary>
            public Node Root
            {
                get { return new Node(_description.root); }
            }

            public override string ToString()
            {
                return _description.ToString();
            }

            public string ToStringConfidence()
            {
                return _description.ToString();
            }
        }
    }
}