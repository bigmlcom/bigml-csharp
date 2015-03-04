using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class Project
    {
        /// <summary>
        /// Orderable properties for Projects
        /// </summary>
        public class Orderable : Orderable<Project>
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