using System.Collections.Generic;

namespace BigML
{
    /// <summary>
    /// A data source or source is the raw data that you want to use to create a predictive model. 
    /// </summary>
    public partial class Source : Response
    {
        /// <summary>
        /// The MIME content-type as provided by your HTTP client. 
        /// The content-type can help BigML.io to better parse your file. 
        /// </summary>
        public string ContentType
        {
            get { return Object.content_type; }
        }

        /// <summary>
        /// A dictionary with an entry per field (column) in your data. 
        /// Each entry includes the column number, the name of the field, and the type of the field.
        /// </summary>
        public IDictionary<string, Field> Fields
        {
            get
            {
                var dictionary = new Dictionary<string, Field>();
                foreach (var kv in Object.fields) dictionary[kv.Key] = new Field(kv.Value);
                return dictionary;
            }
        }

        /// <summary>
        /// The name of the file as you submitted it.
        /// </summary>
        public string FileName
        {
            get { return Object.file_name; }
        }

        /// <summary>
        /// The file MD5 Message-Digest Algorithm as specified by RFC 1321.
        /// </summary>
        public string MD5
        {
            get { return Object.md5; }
        }

        /// <summary>
        /// The name of the source as your provided or the name of the file by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }

        /// <summary>
        /// The current number of datasets that use this source.
        /// </summary>
        public int NumberOfDataSets
        {
            get { return Object.number_of_datasets; }
        }

        /// <summary>
        /// The current number of model that use this source.
        /// </summary>
        public int NumberOfModels
        {
            get { return Object.number_of_models; }
        }

        /// <summary>
        /// The current number of predictions that use this source.
        /// </summary>
        public int NumberOfPredictions
        {
            get { return Object.number_of_predictions; }
        }

        /// <summary>
        /// The number of bytes of the source.
        /// </summary>
        public int Size
        {
            get { return Object.size; }
        }

        /// <summary>
        /// Set of parameters to parse the source.
        /// </summary>
        public Parser SourceParser
        {
            get { return new Parser(Object.source_parser); }
        }

        /// <summary>
        /// A description of the status of the source. 
        /// It includes a code, a message, and some extra information. 
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }

        /// <summary>
        /// The type of source. 
        /// By now, this number defaults to 0. In a future version,
        /// this code will help you identify if this source was created through a file upload, URL, streaming data, etc.
        /// </summary>
        public Type SourceType
        {
            get { return (Type)(int)Object.type; }
        }

        public override string ToString()
        {
            return "{Name: " + this.Name + ", Status: " + this.StatusMessage.StatusCode + "}";
        }
    }
}