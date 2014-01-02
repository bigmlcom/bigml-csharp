using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    public partial class Model
    {
        public class ResultNode
        {

            private float resultConfidence;
            private int resultCount;
            private string resultOutput;

            /// <summary>
            /// The confidence of the output with more weight in this node.
            /// </summary>
            public float Confidence
            {
                set { this.resultConfidence = value; }
                get { return this.resultConfidence; }
            }

            /// <summary>
            /// Number of instances classified by this node.
            /// </summary>
            public int Count
            {
                set { this.resultCount = value; }
                get { return this.resultCount; }
            }

            /// <summary>
            /// Prediction at this node (number or string)
            /// </summary>
            public string Output
            {
                set { this.resultOutput = value; }
                get { return this.resultOutput; }
            }

            public override string ToString() {
                return "{Output: " + this.Output + ", Confidence: " + this.Confidence + "}";
            }

        }
    }
}
