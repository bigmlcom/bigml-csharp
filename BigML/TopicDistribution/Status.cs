using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class TopicDistribution
    {
        /// <summary>
        /// Creating a topic distribution is a near real-time process that take
        /// just a few seconds depending on whether the corresponding topic
        /// model has been used recently and the work load of BigML's systems.
        /// The prediction goes through a number of states until its fully
        /// completed.
        /// Through the status field in the topic distribution you can
        /// determine when the prediction has been fully processed and ready to
        /// be used.
        /// Most of the times topic distributions are fully processed and the
        /// output returned in the first call.
        /// </summary>
        public class Status : Status<TopicDistribution>
        {
            internal Status(JObject status): base(status)
            {
            }
        }
    }
}