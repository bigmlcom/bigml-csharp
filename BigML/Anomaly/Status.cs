using System.Json;

namespace BigML
{
    public partial class Anomaly
    {
        /// <summary>
        /// Creating a cluster is a process that can take just a few seconds or a few days 
        /// depending on the size of the dataset used as input and on the work load of 
        /// BigML's systems. 
        /// The cluster goes through a number of states until its fully completed.
        /// Through the status field in the cluster you can determine when the anomaly has 
        /// been fully processed and ready to be used to create predictions. 
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