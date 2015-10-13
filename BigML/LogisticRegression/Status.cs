using System.Json;

namespace BigML
{
    public partial class LogisticRegression
    {
        /// <summary>
        /// Creating a logistic regression is a process that can take just a few
        /// seconds or a few hours depending on the size of the dataset used as
        /// input and on the work load of BigML's systems.
        /// The logistic regression goes through a number of states until its
        /// fully completed.
        /// Through the status field in the logistic r. you can determine when the
        /// logistic regression has been fully processed and ready to be used to
        /// create prediction.
        /// </summary>
        public class Status : Status<LogisticRegression>
        {
            internal Status(JsonValue status): base(status)
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