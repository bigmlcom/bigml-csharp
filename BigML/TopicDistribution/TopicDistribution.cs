using System;
using System.Json;
using System.Linq;

namespace BigML
{
    /// <summary>
    /// A topicdistribution is created using a topicmodel/id and the new instance (input_data)
    /// that you want to create a topicdistribution for.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/topicdistributions">documentation</a>
    /// website.
    /// </summary>
    public partial class TopicDistribution : Response
    {
        /// <summary>
        /// The dataset/id that was used to build the topicdistribution.
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
        /// A dictionary with an entry per field in the input_data.
        /// Each entry includes the column number in original source, the name of the field, the type of the field, and the specific datatype.
        /// </summary>
        public JsonValue Fields
        {
            get { return Object.fields; }
        }

        /// <summary>
        /// The dictionary of input fields's ids and valued used as input for the topicdistribution.
        /// </summary>
        public JsonValue InputData
        {
            get { return Object.input_data; }
        }

        /// <summary>
        /// The topicdistribution's locale.
        /// </summary>
        public string Locale
        {
            get { return Object.locale; }
        }

        /// <summary>
        /// The topicmodel/id that was used to create the topicdistribution.
        /// </summary>
        public string TopicModel
        {
            get { return Object.model; }
        }

        /// <summary>
        /// Whether the model is still available or has been deleted.
        /// </summary>
        public bool TopicModelStatus
        {
            get { return Object.model_status; }
        }

        /// <summary>
        /// The name of the topicdistribution as you provided or based on the topicmodel's name by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }


        /// <summary>
        /// Objective field with predicted value.
        /// </summary>
        public T GetPredictionOutcome<T>()
        {
            var topicdistribution = Object.output as JsonObject;
            var key = topicdistribution.Keys.First();
            var value = Convert.ChangeType((string)topicdistribution[key],typeof(T));
            return (T)value;
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
        /// A description of the status of the topicdistribution.
        /// It includes a code, a message, and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}