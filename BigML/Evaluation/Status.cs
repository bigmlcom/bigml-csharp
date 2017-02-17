using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Evaluation
    {
        /// <summary>
        /// The evaluation goes through a number of states until its fully completed.
        /// Through the status field in the evaluation you can determine when
        /// the evaluation has been fully processed and ready.
        /// </summary>
        public class Status : Status<Evaluation>
        {
            internal Status(JObject status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the evaluation.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}