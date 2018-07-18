using System.Collections.Generic;

namespace BigML
{
    /// <summary>
    /// Fusions are a special type of composite for which all submodels satisfy
    /// the following constraints: they're all either classifications or
    /// regressions over the same kind of data or compatible fields, with the
    /// same objective field.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/fusions"> documentation</a> website.
    /// </summary>
    public partial class Fusion : Response
    {

        /// <summary>
        /// The name of the Fusion as your provided or based on the name
        /// of the dataset by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }


        /// <summary>
        /// The models that was used to build the fusion.
        /// </summary>
        public List<dynamic> Models
        {
            get { return Object.models; }
        }


        /// <summary>
        /// A description of the status of the fusion.
        /// It includes a code, a message, and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}
