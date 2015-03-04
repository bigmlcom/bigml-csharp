using System.Json;

namespace BigML
{
    public partial class Anomaly
    {
        /// <summary>
        /// Creating an anomaly detector is a process that can take just a few
        /// seconds or a few days depending on the size of the dataset used as
        /// input and on the work load of BigML's systems.
        /// The anomaly detector goes through a number of states until its
        /// fully completed.
        /// Through the status field in the anomaly you can determine when the
        /// anomaly has been fully processed and ready to be used to create scores.
        /// </summary>
        public class Status : Status<Anomaly>
        {
            internal Status(JsonValue status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the anomaly.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}