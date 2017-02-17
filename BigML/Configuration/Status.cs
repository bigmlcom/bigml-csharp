using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Configuration
    {
        /// <summary>
        /// Creating a configuration is a process that can take just a few
        /// seconds. After been created it's ready to be used to create
        /// resources.
        /// </summary>
        public class Status : Status<Configuration>
        {
            internal Status(JObject status): base(status)
            {
            }
        }
    }
}