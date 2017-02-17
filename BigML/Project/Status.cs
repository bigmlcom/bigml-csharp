using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Project
    {
        /// <summary>
        /// Creating an project is a very short process that should take seconds or less
        /// </summary>
        public class Status : Status<Project>
        {
            internal Status(JObject status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the project.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}