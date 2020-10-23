using System.Collections.Generic;
using Newtonsoft.Json.Linq;

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
            get { return new Status(Object.status); }
        }

        /// <summary>
        /// The results of the Execution.
        /// </summary>
        public JArray Results
        {
            get { return Object.execution.results; }
        }

        /// <summary>
        /// The outputs of the Execution.
        /// </summary>
        public JObject Outputs
        {
            get { return Object.outputs; }
        }

        /// <summary>
        /// The inputs used in the Execution.
        /// </summary>
        public JArray Inputs
        {
            get { return Object.inputs; }
        }

        /// <summary>
        /// The outputs per script used in the Execution.
        /// </summary>
        public JObject OutputMaps
        {
            get { return Object.output_maps; }
        }

        /// <summary>
        /// The inputs per script used in the Execution.
        /// </summary>
        public JObject InputMaps
        {
            get { return Object.input_maps; }
        }

        /// <summary>
        /// The result of the Execution.
        /// </summary>
        public dynamic Result
        {
            get { return Object.execution.result; }
        }

    }
}