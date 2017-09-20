using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Forecast
    {
        /// <summary>
        /// Creating a forecast is a near real-time process that take just a
        /// few seconds depending on whether the corresponding timeseries has been
        /// used recently and the work load of BigML's systems. The forecast
        /// goes through a number of states until its fully completed.
        /// Through the status field in the forecast you can determine when
        /// the forecast has been fully processed and ready to be used.
        /// Most of the times forecasts are fully processed and the output
        /// returned in the first call.
        /// </summary>
        public class Status : Status<Forecast>
        {
            internal Status(JObject status): base(status)
            {
            }
        }
    }
}