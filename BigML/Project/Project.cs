using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
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
