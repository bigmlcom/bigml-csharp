using System.Json;

namespace BigML
{
    public partial class Prediction
    {
        public class Predicate
        {
            readonly dynamic _predicate;
            internal Predicate(JsonValue json)
            {
                _predicate = json;
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
