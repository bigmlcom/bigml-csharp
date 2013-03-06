using System.Collections.Generic;

namespace BigML
{
    /// <summary>
    /// A dataset is a structured version of a source where each field has been processed and serialized according to its type. 
    /// </summary>
    public partial class DataSet : Response
    {
        /// <summary>
        /// The number of fields in the dataset.
        /// </summary>
        public int Columns
        {
            get { return Object.columns; }
        }

        /// <summary>
        /// A dictionary with an entry per field (column) in your data. 
        /// Each entry includes the column number, the name of the field, the type of the field, and the summary.
        /// </summary>
        public IDictionary<string, Field> Fields
        {
            get
            {
                var dictionary = new Dictionary<string, Field>();
                foreach (var kv in Object.fields)
                {
                    dictionary[kv.Key] = new Field(kv.Value);
                }
                return dictionary;
            }
        }

        /// <summary>
        /// The dataset's locale.
        /// </summary>
       public string Locale
        {
            get { return Object.locale; }
        }

        /// <summary>
        /// The name of the dataset as your provided or based on the name of the source by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }

        /// <summary>
        /// The current number of models that use this dataset.
        /// </summary>
        public int NumberOfModels
        {
            get { return Object.number_of_models; }
        }

        /// <summary>
        /// The current number of predictions that use this dataset.
        /// </summary>
        public int NumberOfPredictions
        {
            get { return Object.number_of_predictions; }
        }

        /// <summary>
        /// The total number of rows in the dataset.
        /// </summary>
        public int Rows
        {
            get { return Object.rows; }
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
        public string Source
        {
            get { return Object.source; }
        }

        /// <summary>
        /// Whether the source is still available or has been deleted.
        /// </summary>
        public bool SourceStatus
        {
            get { return Object.source_status; }
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