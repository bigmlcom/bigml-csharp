using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Execution
    {
        /// <summary>
        /// The execution goes through a number of states until is completed.
        /// Through the status field in the executions you can determine when
        /// the execution has been fully executed and ready to be used to
        /// create a model. 
        /// </summary>
        public class Status : Status<Execution>
        {
            internal Status(JObject json)
                : base(json)
            {
            }

            /// <summary>
            /// Number of bytes processed so far.
            /// </summary>
            public int Bytes
            {
                get { return _status.bytes; }
            }
        }
    }
}
