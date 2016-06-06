using BigML.Meta;

namespace BigML
{
    public partial class Script
    {
        /// <summary>
        /// Filterable properties for Scripts
        /// </summary>
        public class Filterable : Filterable<Script>
        {
           

            /// <summary>
            /// The current number of models that use this source.
            /// </summary>
            public Int NumberOfExecutions
            {
                get { return Object.number_of_models; }
            }

           
        }
    }
}
