using System.Json;

namespace BigML
{
    public partial class Sample
    {
        /// <summary>
        /// Creating a dataset sample is a process that can take just a few
        /// seconds or a few hours depending on the size of the dataset used as
        /// input and on the work load of BigML's systems.
        /// The sample goes through a number of states until its completed.
        /// Through the status field in the sample you can determine when the
        /// sample has been fully processed and ready to be used in scatterplot.
        /// </summary>
        public class Status : Status<Sample>
        {
            internal Status(JsonValue status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the sample.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}