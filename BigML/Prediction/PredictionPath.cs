using System.Collections.Generic;
using System.Json;

namespace BigML
{
    public partial class Prediction
    {
        /// <summary>
        /// A Prediction Path Object specifying the decision path followed to make the prediction, the next predicates, and lists of unknown fields and bad fields submitted.
        /// </summary>
        public class PredictionPath
        {
            readonly dynamic _path;
            protected PredictionPath(JsonValue json)
            {
                _path = json;
            }

            /// <summary>
            /// A collection of field's ids with wrong values submitted. Bad fields are ignored. 
            /// That is, if you submit a value that is wrong, a prediction is created anyway ignoring the input field with the wrong value.
            /// </summary>
            public IEnumerable<string> BadFields
            {
                get
                {
                    return (_path.bad_fields as JsonValue).Select(field => (string) field);
                }
            }

            /// <summary>
            /// A collection of Predicate Objects with the children of the deepest node that was reached with the input_data
            /// </summary>
            public IEnumerable<Predicate> NextPredicates
            {
                get
                {
                    return (_path.next_predicate as JsonValue).Select(predicate => new Predicate(predicate));
                }
            }

            /// <summary>
            /// An ordered collection of Predicate Objects in the decision path from the root to the current node or to a final decision if the the next predicate array is empty.
            /// </summary>
            public IEnumerable<Predicate> Path
            {
                get
                {
                    return (_path.path as JsonValue).Select(predicate => new Predicate(predicate));
                }
            }

            public IEnumerable<string> UnknownFields
            {
                get
                {
                    return (_path.unknown_fields as JsonValue).Select(field => (string)field);
                }
            }
        }
    }
}