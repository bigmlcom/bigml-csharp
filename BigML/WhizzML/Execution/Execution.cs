using System.Collections.Generic;

namespace BigML
{
    /// <summary>
    /// Once a WhizzML script has been created, you can execute it as many
    /// times as you want. Every execution will return a list of outputs and/or
    /// BigML resources (models, ensembles, clusters, predictions, etc.) that
    /// were created during the given run. It's also possible to execute a
    /// pipeline of more than one scripts in one request.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/executions">documentation</a>
    /// website.
    /// </summary>
    public partial class Execution : Response
    {


        /// <summary>
        /// The name of the execution as your provided or based on the name of
        /// the script by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }



        /// <summary>
        /// A description of the status of the execution.
        /// </summary>
        public Status StatusMessage
        {
            get{ return new Status(Object.status); }
        }
    }
}