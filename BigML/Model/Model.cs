using System.Collections.Generic;
using System.Json;

namespace BigML
{
    /// <summary>
    /// A model is a tree-like representation of your dataset with predictive
    /// power.
    /// You can create a model selecting which fields from your dataset you want
    /// to use as input fields (or predictors) and which field you do want to
    /// predict, the objective field.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/developers/models">developers</a>
    /// website.
    /// </summary>
    public partial class Model : Response
    {
        /// <summary>
        /// The number of fields in the model.
        /// </summary>
        public int Columns
        {
            get { return Object.columns;  }
        }

        /// <summary>
        /// The dataset/id that was used to build the dataset.
        /// </summary>
        public string DataSet
        {
            get { return Object.dataset;  }
        }

        /// <summary>
        /// Whether the dataset is still available or has been deleted.
        /// </summary>
        public bool DataSetStatus 
        {
            get { return Object.dataset_status;  }
        }

        /// <summary>
        /// The holdout size used to build the model.
        /// </summary>
        public double Holdout
        {
            get { return Object.holdout; }
        }

        /// <summary>
        /// The list of input fields's ids used to build the model.
        /// </summary>
        public IEnumerable<string> InputFields
        {
            get
            {
                return (Object.input_fields as JsonValue).Select(field => (string)field);
            }
        }

        /// <summary>
        /// Dataset's locale
        /// </summary>
        public string Locale
        {
            get { return Object.locale;  }
        }

        /// <summary>
        /// The total number of fields in the dataset used to build the model.
        /// </summary>
        public int MaxColumns 
        {
            get { return Object.max_columns; }
        }

        /// <summary>
        /// The total number of instances in the dataset used to build the model.
        /// </summary>
        public int MaxRows
        {
            get { return Object.max_rows; }
        }

        /// <summary>
        /// All the information that you need to recreate or use the model on your own. 
        /// It includes a very intuitive description of the tree-like structure that made the model up and the field's dictionary describing the fields and their summaries.
        /// </summary>
        public Description ModelDescription
        {
            get { return new Description(Object.model, InputFields); }
        }

        /// <summary>
        /// The name of the model as your provided or based on the name of the dataset by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }

        /// <summary>
        /// The current number of predictions that use this model.
        /// </summary>
        public int NumberOfPredictions
        {
            get { return Object.number_of_predictions; }
        }

        /// <summary>
        /// Specifies the id of the field that the model predicts. 
        /// </summary>
        public string ObjectiveField
        {
            get { return Object.objective_fields[0]; }
        }

        /// <summary>
        /// The range of instances used to build the model.
        /// </summary>
        public IEnumerable<int> Range 
        { 
            get
            { 
                return (Object.range as JsonValue).Select(r => (int)r);
            }
        }

        /// <summary>
        /// The total number of instances used to build the model.
        /// </summary>
        public int Rows 
        { 
            get { return Object.rows; }
        }

        /// <summary>
        /// The number of bytes of the dataset that were used to create this model.
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
            get { return Object.source_id; }
        }

        /// <summary>
        /// Whether the source is still available or has been deleted.
        /// </summary>
        public bool SourceStatus
        {
            get { return Object.source_status; }
        }

        /// <summary>
        /// A description of the status of the model. It includes a code, a message, and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}