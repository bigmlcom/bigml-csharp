using System.Collections.Generic;

namespace BigML
{
    /// <summary>
    /// A library is a special kind of compiled WhizzML source code that only
    /// defines functions and constants. It is intended as an import for
    /// executable scripts.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/libraries">documentation</a>
    /// website.
    /// </summary>
    public partial class Library : Response
    {
        
        
        /// <summary>
        /// The name of the library as your provided or the default one.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }

        /// <summary>
        /// The number of bytes of the source code that were used to create this library.
        /// </summary>
        public int Size
        {
            get { return Object.size; }
        }

        /// <summary>
        /// The source code that was used to build the library.
        /// </summary>
        public string SourceCode
        {
            get { return Object.source_code; }
        }


        /// <summary>
        /// A description of the status of the library.
        /// </summary>
        public Status StatusMessage
        {
            get{ return new Status(Object.status); }
        }
    }
}