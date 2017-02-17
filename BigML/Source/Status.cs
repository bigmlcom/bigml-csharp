using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Source
    {
        /// <summary>
        /// Before a source is successfully created, BigML.io makes sure that
        /// it has been uploaded in an understandable format, that the data
        /// that it contains is parseable, and that the types for each column
        /// in the data can be inferred successfully. 
        /// The source goes through a number of states until all these analyses
        /// are completed. Through the status field in the source you can
        /// determine when the source has been fully processed and ready to be
        /// used to create a dataset.
        /// </summary>
        public class Status : Status<Source>
        {
            internal Status(JObject json)
                : base(json)
            {
            }
        }
    }
}