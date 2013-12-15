using System;
using System.Collections.Generic;
using System.Json;
using System.Linq.Expressions;

namespace BigML
{
    public partial class Model
    {
        /// <summary>
        /// Predicate structure to make a decision at each node.
        /// </summary>
        public class Predicate
        {
            readonly dynamic _predicate;
            internal Predicate(JsonValue json)
            {
                _predicate = json;
            }

            internal Expression Expression(Dictionary<string, ParameterExpression> parameters)
            {
                if (IsSimple)
                {
                    return System.Linq.Expressions.Expression.Constant(Constant);
                }
                var value = default(double);
                var x = Value as object;
                if (double.TryParse(Value, out value)) x = value;

                switch (Operator)
                {
                    case ">":

                        return System.Linq.Expressions.Expression.GreaterThan(parameters[Field],
                                                                              System.Linq.Expressions.Expression.
                                                                                  Constant(x));
                    case ">=":
                        return System.Linq.Expressions.Expression.GreaterThanOrEqual(parameters[Field],
                                                                                     System.Linq.Expressions.
                                                                                         Expression.
                                                                                         Constant(x));
                    case "<":
                        return System.Linq.Expressions.Expression.LessThan(parameters[Field],
                                                                           System.Linq.Expressions.Expression.
                                                                               Constant(Value));
                    case "<=":
                        return System.Linq.Expressions.Expression.LessThanOrEqual(parameters[Field],
                                                                                  System.Linq.Expressions.Expression.
                                                                                      Constant(x));
                    case "=":
                        return System.Linq.Expressions.Expression.Equal(parameters[Field],
                                                                        System.Linq.Expressions.Expression.
                                                                            Constant(x));
                    default:
                        throw new Exception("unknown operator");
                }
            }


            internal Expression ConfidenceVal(Dictionary<string, ParameterExpression> parameters)
            {
                if (IsSimple)
                {
                    return System.Linq.Expressions.Expression.Constant(Constant);
                }
                var value = default(double);
                var x = Value as object;
                if (double.TryParse(Value, out value)) x = value;

                switch (Operator)
                {
                    case ">":

                        return System.Linq.Expressions.Expression.GreaterThan(parameters[Field],
                                                                              System.Linq.Expressions.Expression.
                                                                                  Constant(x));
                    case ">=":
                        return System.Linq.Expressions.Expression.GreaterThanOrEqual(parameters[Field],
                                                                                     System.Linq.Expressions.
                                                                                         Expression.
                                                                                         Constant(x));
                    case "<":
                        return System.Linq.Expressions.Expression.LessThan(parameters[Field],
                                                                           System.Linq.Expressions.Expression.
                                                                               Constant(Value));
                    case "<=":
                        return System.Linq.Expressions.Expression.LessThanOrEqual(parameters[Field],
                                                                                  System.Linq.Expressions.Expression.
                                                                                      Constant(x));
                    case "=":
                        return System.Linq.Expressions.Expression.Equal(parameters[Field],
                                                                        System.Linq.Expressions.Expression.
                                                                            Constant(x));
                    default:
                        throw new Exception("unknown operator");
                }
            }

            /// <summary>
            /// Is leaf node (boolean)
            /// </summary>
            public bool IsSimple
            {
                get { return _predicate.JsonType == JsonType.Boolean; }
            }

            public bool Constant
            {
                get { return _predicate; }
            }

            /// <summary>
            /// Field's id used for this decision.
            /// </summary>
            public string Field
            {
                get { return _predicate.field; }
            }

            /// <summary>
            /// Type of test used for this field.
            /// </summary>
            public string Operator
            {
                get { return _predicate.@operator; }
            }

            /// <summary>
            /// Value of the field to make this node decision (number or string)
            /// </summary>
            public string Value
            {
                get { return _predicate.value; }
            }
        }
    }
}