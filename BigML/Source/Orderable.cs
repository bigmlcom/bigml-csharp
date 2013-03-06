using BigML.Meta;
    
namespace BigML
{
    using Meta.Key;

    public partial class Source
    {
        /// <summary>
        /// Orderable properties on Source
        /// </summary>
        public class Orderable : Orderable<Source>
        {
            /// <summary>
            /// This is the MIME content-type as provided by your HTTP client. 
            /// The content-type can help BigML.io to better parse your file
            /// </summary>
            public String ContentType
            {
                get { return Object.content_type; }
            }

            /// <summary>
            /// The name of the file as you submitted it.
            /// </summary>
            public String FileName
            {
                get { return Object.file_name; }
            }

            /// <summary>
            /// The current number of datasets that use this source
            /// </summary>
            public Int NumberOfDatasets
            {
                get { return Object.number_of_datasets; }
            }

            /// <summary>
            /// The current number of models that use this source.
            /// </summary>
            public Int NumberOfModels
            {
                get { return Object.number_of_models; }
            }

            /// <summary>
            /// The current number of predictions that use this source.
            /// </summary>
            public Int NumberOfPredictions
            {
                get { return Object.number_of_predictions; }
            }

            /// <summary>
            /// The type of source.
            /// </summary>
            public Meta.Key.Type SourceType
            {
                get { return Object.type; }
            }
        }
    }
}