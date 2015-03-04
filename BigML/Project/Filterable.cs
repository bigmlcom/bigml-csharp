using BigML.Meta;

namespace BigML
{
    public partial class Project
    {
        /// <summary>
        /// Filterable properties for Projects
        /// </summary>
        public class Filterable : Filterable<Project>
        {

            /// <summary>
            /// Whether the project is completed or in a different status.
            /// </summary>
            public Bool Status
            {
                get { return Object.status; }
            }
        }
    }
}
