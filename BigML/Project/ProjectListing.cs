using System;
using System.Threading.Tasks;

namespace BigML
{
    public class ProjectListing : Query<Project.Filterable, Project.Orderable, Project>
    {
        public ProjectListing(Func<string, Task<Listing<Project>>> client)
            : base(client)
        {
        }
    }
}
