using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Response
    {
        /// <summary>
        /// Abstract base class for all status messages returned by BigML.
        /// </summary>
        public abstract class Status<T> where T : Response
        {
            protected readonly dynamic _status;

            protected Status(JObject json)
            {
                _status = json;
            }

            /// <summary>
            /// A status or error code that reflects the status of the resource.
            /// </summary>
            public Code StatusCode
            {
                get { return (Code)(int)_status.code; }
            }

            /// <summary>
            /// Number of milliseconds that BigML.io took to process the resource.
            /// </summary>
            public int Elapsed
            {
                get { return _status.elapsed; }
            }

            /// <summary>
            /// A human readable message explaining the status.
            /// </summary>
            public string Message
            {
                get { return _status.message; }
            }

            /// <summary>
            /// Extra Information
            /// </summary>
            /*public IEnumerable<string> Extra
            {
                get { return (_status.extra as JValue).Select(e => (string)e); }
            }*/

            protected bool IsFinished()
            {
                return (StatusCode == BigML.Code.Finished);
            }

            public bool NotSuccessOrFail()
            {
                return (StatusCode != BigML.Code.Finished) && (StatusCode > 0);
            }

            public bool IsSuccessOrFail()
            {
                return !this.NotSuccessOrFail();
            }

            public override string ToString()
            {
                return _status.ToString();
            }
        }
    }
}