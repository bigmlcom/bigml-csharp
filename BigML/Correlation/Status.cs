using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Correlation
    {
        /// <summary>
        /// Creating a correlation is a process that can take just a few
        /// seconds or a few days depending on the size of the dataset used as
        /// input and on the work load of BigML's systems.
        /// The correlation goes through a number of states until its
        /// fully completed.
        /// Through the status field in the correlation you can determine
        /// when the correlation has been fully completed.
        /// </summary>
        public class Status : Status<Correlation>
        {
            internal Status(JObject status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the correlation.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}