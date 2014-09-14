using System.Json;

namespace BigML
{
    public partial class Ensemble
    {
        /// <summary>
        /// Creating an ensemble is a process that can take just a few seconds or a few days 
        /// depending on the size of the dataset used as input and on the work load of 
        /// BigML's systems. 
        /// The ensemble goes through a number of states until its fully completed.
        /// Through the status field in the ensemble you can determine when the anomaly has 
        /// been fully processed and ready to be used to create predictions. 
        /// </summary>
        public class Status : Status<Ensemble>
        {
            internal Status(JsonValue status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the ensemble.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}