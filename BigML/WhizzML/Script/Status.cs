using System.Json;

namespace BigML
{
    public partial class Script
    {
        /// <summary>
        /// Before a script is successfully created, BigML.io makes sure that
        /// it has a valid source code. 
        /// The script goes through a number of states until all these
        /// analyses are completed and it's ready for be used by executed.
        /// </summary>
        public class Status : Status<Script>
        {
            internal Status(JsonValue json)
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