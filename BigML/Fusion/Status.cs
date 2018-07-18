using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Fusion
    {
        /// <summary>
        /// Creating a Fusion is a process that can take just a few
        /// seconds. This time depends on the quantity of models used
        /// and on the work load of BigML's systems.
        /// The Fusion goes through a number of states until its fully
        /// completed.
        /// Through the status field in the Fusion you can determine
        /// when the Fusion has been fully completed.
        /// </summary>
        public class Status : Status<Fusion>
        {
            internal Status(JObject status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the fusion.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}