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
            /// Which is project's category.
            /// </summary>
            public Category category
            {
                get { return Object.category; }
            }
        }
    }
}