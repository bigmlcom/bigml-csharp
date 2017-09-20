using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class TimeSeries
    {
        /// <summary>
        /// Creating a time series is a process that can take just a few
        /// seconds or a few hours depending on the size of the dataset used as
        /// input and on the work load of BigML's systems.
        /// The time series goes through a number of states until its
        /// fully completed.
        /// Through the status field in time series you can determine
        /// when it has been fully processed and is ready to be used to create
        /// forecast predictions.
        /// </summary>
        public class Status : Status<TimeSeries>
        {
            internal Status(JObject status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the logistic regression.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}