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
            /// Which is project's category.
            /// </summary>
            public Category category
            {
                get { return Object.category; }
            }
        }
    }
}
