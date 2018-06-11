using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class OptiML
    {
        /// <summary>
        /// Creating an OptiML is a process that can take just a few
        /// seconds or a few hours depending on the size of the dataset used as
        /// input and on the work load of BigML's systems.
        /// The OptiML goes through a number of states until its completed.
        /// Through the status field in the OptiML you can determine when the
        /// OptiML has been fully processed and ready to be used in scatterplot.
        /// </summary>
        public class Status : Status<OptiML>
        {
            internal Status(JObject status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the OptiML.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}