using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class AnomalyScore
    {
        /// <summary>
        /// The score goes through a number of states until its fully completed.
        /// Through the status field in the score you can determine when the score has 
        /// been fully processed and ready to be used to create predictions. 
        /// </summary>
        public class Status : Status<AnomalyScore>
        {
            internal Status(JObject status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the anomaly score.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}