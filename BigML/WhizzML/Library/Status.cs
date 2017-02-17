using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Library
    {
        /// <summary>
        /// Before a library is successfully created, BigML.io makes sure that
        /// it has a valid source code. 
        /// The library goes through a number of states until all these
        /// analyses are completed and it's ready for be used by scripts or
        /// other libraries.
        /// </summary>
        public class Status : Status<Library>
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