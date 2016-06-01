using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class Script
    {
        /// <summary>
        /// Orderable properties for scripts
        /// </summary>
        public class Orderable : Orderable<Script>
        {
         
            /// <summary>
            /// The current number of models that use this source.
            /// </summary>
            public Int NumberOfExecutions
            {
                get { return Object.number_of_executions; }
            }

           
        }
    }
}