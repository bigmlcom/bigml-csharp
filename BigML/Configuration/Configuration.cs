using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// A configuration is a helper resource that provides an easy way to reuse
    /// the same arguments during the resource creation.
    /// A configuration must have a name and optionally a category,
    /// description, and multiple tags to help you organize and retrieve your
    /// configurations.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/configurations">documentation</a>
    /// website.
    /// </summary>
    public partial class Configuration : Response
    {

        /// <summary>
        /// The name of the configuration as your provided.
        /// Defaut is "Configuration <Number>"
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }

    }
}
