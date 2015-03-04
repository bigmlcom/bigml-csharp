using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a project using supplied arguments.
        /// </summary>
        public Task<Project> CreateProject(Project.Arguments arguments)
        {
            return Create<Project>(arguments);
        }


        /// <summary>
        /// List all the projects
        /// </summary>
        public Query<Project.Filterable, Project.Orderable, Project> ListProjects()
        {
            return new ProjectListing(List<Project>);
        }
    }
}