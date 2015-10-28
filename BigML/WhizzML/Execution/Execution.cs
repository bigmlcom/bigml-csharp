using System.Collections.Generic;

namespace BigML
{
    /// <summary>
    /// A dataset is a structured version of a source where each field has been
    /// processed and serialized according to its type.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/developers/executions">developers</a>
    /// website.
    /// </summary>
    public partial class Execution : Response
    {
        
        
        /// <summary>
        /// The name of the dataset as your provided or based on the name of the source by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }

        /// <summary>
        /// The number of bytes of the source that were used to create this dataset.
        /// </summary>
        public int Size
        {
            get { return Object.size; }
        }

        /// <summary>
        /// The source/id that was used to build the dataset.
        /// </summary>
        public string SourceCode
        {
            get { return Object.sourceCode; }
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