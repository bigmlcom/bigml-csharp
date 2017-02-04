using System.Json;

namespace BigML
{
    public partial class AssociationSet
    {
        /// <summary>
        /// The association set goes through a number of states until its fully completed.
        /// Through the status field in the resource you can determine when the set has 
        /// been fully processed and ready. 
        /// </summary>
        public class Status : Status<AssociationSet>
        {
            internal Status(JsonValue status): base(status)
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