using System.Json;

namespace BigML
{
    public partial class Cluster
    {
        /// <summary>
        /// Creating a cluster is a process that can take just a few seconds or
        /// a few days depending on the size of the dataset used as input and
        /// on the work load of BigML's systems.
        /// The cluster goes through a number of states until its fully completed.
        /// Through the status field in the cluster you can determine when the
        /// cluster has been fully processed and ready to be used to create centroids.
        /// </summary>
        public class Status : Status<Cluster>
        {
            internal Status(JsonValue status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the cluster.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}