using System.Json;

namespace BigML
{
    public partial class Prediction
    {
        /// <summary>
        /// Creating a prediction is a near real-time process that take just a few seconds depending on whether the corresponding model has been used recently 
        /// and the work load of BigML's systems. The prediction goes through a number of states until its fully completed. 
        /// Through the status field in the prediction you can determine when the prediction has been fully processed and ready to be used. 
        /// Most of the times predictions are fully processed and the output returned in the first call. 
        /// </summary>
        public class Status : Status<Prediction>
        {
            internal Status(JsonValue status): base(status)
            {
            }
        }
    }
}