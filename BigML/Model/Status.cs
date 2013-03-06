using System.Json;

namespace BigML
{
    public partial class Model
    {
        /// <summary>
        /// Creating a model is a process that can take just a few seconds or a few days depending on the size of the dataset used as input and on the work load of BigML's systems. 
        /// The model goes through a number of states until its fully completed. 
        /// Through the status field in the model you can determine when the model has been fully processed and ready to be used to create predictions. 
        /// </summary>
        public class Status : Status<Model>
        {
            internal Status(JsonValue status): base(status)
            {
            }

            /// <summary>
            /// How far BigML.io has progressed processing the model.
            /// </summary>
            public double Progress
            {
                get { return _status.progress; }
            }

        }
    }
}