using System.Json;

namespace BigML
{
    public partial class Association
    {
        /// <summary>
        /// Creating an association discovery is a process that can take just a
        /// few seconds or a few days depending on the size of the dataset used
        /// as input and on the work load of BigML's systems.
        /// The association goes through a number of states until its fully completed.
        /// </summary>
        public class Status : Status<Association>
        {
            internal Status(JsonValue status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the association.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}