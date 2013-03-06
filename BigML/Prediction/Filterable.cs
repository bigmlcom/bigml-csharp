using BigML.Meta;

namespace BigML
{
    public partial class Prediction
    {
        /// <summary>
        /// Filterable properties for Prediction.
        /// </summary>
        public abstract class Filterable : Filterable<Prediction>
        {
            /// <summary>
            /// The dataset/id that was used to build the model.
            /// </summary>
            public String DataSet
            {
                get { return Object.dataset; }
            }

            /// <summary>
            /// Whether the dataset is still available or has been deleted.
            /// </summary>
            public Bool DataStatus
            {
                get { return Object.dataset_status; }
            }

            /// <summary>
            /// The model/id that was used to build the prediction.
            /// </summary>
            public String Model
            {
                get { return Object.model; }
            }

            /// <summary>
            /// Whether the model is still available or has been deleted.
            /// </summary>
            public Bool ModelStatus
            {
                get { return Object.model_status; }
            }

            /// <summary>
            /// The objective field of the prediction.
            /// </summary>
            //[Name("objective_fields")]
            //public String ObjectiveField { get { return Object.model_status; } }

            /// <summary>
            /// The objective field of the prediction.
            /// </summary>
            //[Name("prediction")]
            //public String Prediction { get { return Object.model_status; } }

            /// <summary>
            /// The source/id that was used to build the dataset.
            /// </summary>
            public String Source
            {
                get { return Object.source; }
            }

            /// <summary>
            /// Whether the source is still available or has been deleted.
            /// </summary>
            public Bool SourceStatus
            {
                get { return Object.source_status; }
            }
        }
    }
}