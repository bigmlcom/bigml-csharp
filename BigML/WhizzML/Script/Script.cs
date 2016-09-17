using System.Collections.Generic;

namespace BigML
{
    /// <summary>
    /// A script is compiled source code written in WhizzML, BigML's custom
    /// scripting language for automating Machine Learning workflows.
    /// Once a script has been created and compiled, it can be used as an input
    /// for an execution resource.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/scripts">documentation</a> website.
    /// </summary>
    public partial class Script : Response
    {
        
        /// <summary>
        /// The name of the script as your provided or the default one.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }

        /// <summary>
        /// The current number of models that use this dataset.
        /// </summary>
        public int NumberOfExecutions
        {
            get { return Object.number_of_models; }
        }

     
        /// <summary>
        /// The source code that was used to build the script.
        /// </summary>
        public string SourceCode
        {
            get { return Object.source_code; }
        }


        /// <summary>
        /// A description of the status of the dataset.
        /// </summary>
        public Status StatusMessage
        {
            get{ return new Status(Object.status); }
        }
    }
}