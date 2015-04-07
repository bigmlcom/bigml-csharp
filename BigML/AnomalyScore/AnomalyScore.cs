using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// An anomaly score is created from an anomaly and the input_data for
    /// which you wish to create an anomaly score. New anomaly score will be
    /// between 0 and 1.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/developers/anomalyscores">developers</a>
    /// website.
    /// </summary>
    public partial class AnomalyScore : Response
    {

        /// <summary>
        /// The name of the anomaly score as your provided or based on the name
        /// of the anomaly by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }


        /// <summary>
        /// The anomaly/id that was used to build the score.
        /// </summary>
        public string Anomaly
        {
            get { return Object.anomaly; }
        }



        /// <summary>
        /// Whether the anomaly is still available or has been deleted.
        /// </summary>
        public bool AnomalyStatus
        {
            get { return Object.anomaly_status; }
        }


        /// <summary>
        /// A description of the status of the anomaly score. It includes a
        /// code, a message, and some extra information.
        /// </summary>
        public Status<AnomalyScore> StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}
