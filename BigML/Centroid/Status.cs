using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Centroid
    {
        /// <summary>
        /// Creating a centroid is a process that can take just a few seconds
        /// or a few days depending on the size of the dataset used as input
        /// and on the work load of BigML's systems.
        /// The centroid goes through a number of states until its fully
        /// completed.
        /// Through the status field in the centroid you can determine when the
        /// centroid has been fully processed.
        /// </summary>
        public class Status : Status<Centroid>
        {
            internal Status(JObject status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the centroid.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}