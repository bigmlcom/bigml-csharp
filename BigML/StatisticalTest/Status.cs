using System.Json;

namespace BigML
{
    public partial class StatisticalTest
    {
        /// <summary>
        /// Creating a correlation is a process that can take just a few
        /// seconds or a few days depending on the size of the dataset used as
        /// input and on the work load of BigML's systems.
        /// The statistical test goes through a number of states until its
        /// fully completed.
        /// Through the status field in the statistical test you can determine
        /// when the statistical test has been fully completed.
        /// </summary>
        public class Status : Status<StatisticalTest>
        {
            internal Status(JsonValue status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the statisticaltest.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}