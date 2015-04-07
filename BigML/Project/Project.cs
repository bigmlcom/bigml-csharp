using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// A project is an abstract resource that helps you group related BigML
    /// resources together. A project must have a name and optionally a
    /// category, description, and tags to help you organize and retrieve it.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/developers/projects">developers</a>
    /// website.
    /// </summary>
    public partial class Project : Response
    {

        /// <summary>
        /// The name of the project as your provided or based on the default 
        /// name.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }

        /// <summary>
        /// A description of the status of the project. 
        /// It includes a code, a message, and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}
