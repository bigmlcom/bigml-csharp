using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Model
    {
        /// <summary>
        /// A tree-like recursive structure representing the boosted model.
        /// </summary>
        public class BoostedNode
        {
            readonly dynamic _node;
            internal BoostedNode(JObject json)
            {
                _node = json;
            }

            /// <summary>
            /// Array of child BoostedNode Objects.
            /// </summary>
            List<BoostedNode> _children;
            public IEnumerable<BoostedNode> Children
            {
                get
                {
                    if (_children == null)
                    {
                        _children = new List<BoostedNode>();
                        if (_node.children != null) {
                            foreach (JObject child in _node.children)
                            {
                                _children.Add(new BoostedNode(child));

                            }
                        }
                    }
                    return _children;
                }
            }

            /// <summary>
            ///
            /// </summary>
            public float gSum
            {
                get { return _node.g_sum; }
            }

            /// <summary>
            ///
            /// </summary>
            public float hSum
            {
                get { return _node.h_sum; }
            }

            /// <summary>
            /// Number of instances classified by this node.
            /// </summary>
            public int Count
            {
                get { return _node.count; }
            }

            /// <summary>
            /// Prediction at this node (number or string)
            /// </summary>
            public dynamic Output
            {
                get { return _node.output.Value; }
            }

            private Predicate _predicate;
            /// <summary>
            /// Predicate structure to make a decision at this node.
            /// </summary>
            /// 
            public Predicate Predicate
            {
                get {
                    if (_predicate == null)
                    {
                        _predicate = new Predicate(_node.predicate);
                    }
                    return _predicate;
                }
            }

            public Dictionary<object, object> toDictionary()
            {
                Dictionary<object, object> dictionaryResult = new Dictionary<object, object>();
                dictionaryResult.Add("prediction", this.Output);
                dictionaryResult.Add("gSum", this.gSum);
                dictionaryResult.Add("hSum", this.hSum);
                dictionaryResult.Add("count", this.Count);

                return dictionaryResult;
            }

            public override string ToString()
            {
                return Output;
            }
        }
    }
}