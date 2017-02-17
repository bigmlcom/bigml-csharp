using System.Collections.Generic;
using Newtonsoft.Json.Linq;


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
            protected PredictionPath(JObject json)
            {
                _path = json;
            }

            /// <summary>
            /// A collection of field's ids with wrong values submitted. Bad fields are ignored. 
            /// That is, if you submit a value that is wrong, a prediction is created anyway ignoring the input field with the wrong value.
            /// </summary>
            List<string> _bad_fieds;
            public IEnumerable<string> BadFields
            {
                get
                {
                    if (_bad_fieds == null) {
                        _bad_fieds = new List<string>();
                    }
                    if (_path.bad_fields != null)
                    {
                        foreach (string field in _path.bad_fields)
                        {
                            _bad_fieds.Add(field);
                        }
                    }
                    return _bad_fieds;
                }
            }

            /// <summary>
            /// A collection of Predicate Objects with the children of the deepest node that was reached with the input_data
            /// </summary>
            List<Predicate> _nextPredicates;
            public IEnumerable<Predicate> NextPredicates
            {
                get
                {
                    if (_nextPredicates == null)
                    {
                        _nextPredicates = new List<Predicate>();
                    }
                    if (_path.next_predicate != null)
                    {
                        foreach (Predicate predicate in _path.next_predicate)
                        {
                            _nextPredicates.Add(predicate);
                        }
                    }
                    return _nextPredicates;
                }
            }

            /// <summary>
            /// An ordered collection of Predicate Objects in the decision path
            /// from the root to the current node or to a final decision if the
            /// next predicate array is empty.
            /// </summary>
            List<Predicate> _pathPath;
            public IEnumerable<Predicate> Path
            {
                get
                {
                    if (_pathPath == null)
                    {
                        _pathPath = new List<Predicate>();
                    }
                    if (_path.path != null)
                    {
                        foreach (Predicate step in _path.path)
                        {
                            _pathPath.Add(step);
                        }
                    }
                    return _pathPath;
                }
            }

            List<string> _unknownFields;
            public IEnumerable<string> UnknownFields
            {
                get
                {
                    if (_unknownFields == null)
                    {
                        _unknownFields = new List<string>();
                    }
                    if (_path.unknown_fields != null)
                    {
                        foreach (string fieldId in _path._unknownFields)
                        {
                            _unknownFields.Add(fieldId);
                        }
                    }
                    return _unknownFields;
                }
            }
        }
    }
}