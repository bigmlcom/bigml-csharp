using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Deepnet
    {
        /// <summary>
        /// Creating a Deepnet is a process that can take just a few
        /// seconds or a few days depending on the size of the dataset used as
        /// input and on the work load of BigML's systems.
        /// The Deepnet goes through a number of states until its
        /// fully completed.
        /// Through the status field in the Deepnet you can determine
        /// when the Deepnet has been fully completed.
        /// </summary>
        public class Status : Status<Deepnet>
        {
            internal Status(JObject status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the deepnet.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}