using System;
using System.Json;
using System.Linq;

namespace BigML
{
    /// <summary>
    /// A prediction is created using a model/id and the new instance (input_data) that you want to create a prediction for.
    /// </summary>
    public partial class Prediction : Response
    {
        /// <summary>
        /// The dataset/id that was used to build the prediction.
        /// </summary>
        public string DataSet
        {
            get { return Object.dataset; }
        }

        /// <summary>
        /// Whether the dataset is still available or has been deleted.
        /// </summary>
        public bool DataSetStatus
        {
            get { return Object.dataset_status; }
        }

        /// <summary>
        /// A dictionary with an entry per field in the input_data or prediction_path. 
        /// Each entry includes the column number in original source, the name of the field, the type of the field, and the specific datatype.
        /// </summary>
        public JsonValue Fields
        {
            get { return Object.fields; }
        }

        /// <summary>
        /// The dictionary of input fields's ids and valued used as input for the prediction.
        /// </summary>
        public JsonValue InputData
        {
            get { return Object.input_data; }
        }

        /// <summary>
        /// The prediction's locale.
        /// </summary>
        public string Locale
        {
            get { return Object.locale; }
        }

        /// <summary>
        /// The model/id that was used to create the prediction.
        /// </summary>
        public string Model
        {
            get { return Object.model; }
        }

        /// <summary>
        /// Whether the model is still available or has been deleted.
        /// </summary>
        public bool ModelStatus
        {
            get { return Object.model_status; }
        }

        /// <summary>
        /// The name of the prediction as you provided or based on the name of the objective field's name by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }

        /// <summary>
        /// Specifies the id of the field that the model predicts. 
        /// </summary>
        public string ObjectiveField
        {
            get { return Object.objective_field[0]; }
        }

        /// <summary>
        /// Objective field with predicted value.
        /// </summary>
        public T GetPredictionOutcome<T>()
        {
            var prediction =  Object.prediction as JsonObject;
            var key = prediction.Keys.First();
            var value = Convert.ChangeType((string)prediction[key],typeof(T));
            return (T)value;
        }

        /// <summary>
        /// A Prediction Path Object specifying the decision path followed to make the prediction, the next predicates, and lists of unknown fields and bad fields submitted .
        /// </summary>
        public JsonValue Path
        {
            get { return Object.prediction_path; }
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
        /// A description of the status of the prediction. 
        /// It includes a code, a message, and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}