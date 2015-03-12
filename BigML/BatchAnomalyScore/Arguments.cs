using System.Collections.Generic;
using System.Json;
using System.Linq;

namespace BigML
{
    public partial class BatchAnomalyScore
    {
        public class Arguments : Arguments<BatchAnomalyScore>
        {
            public Arguments()
            {

            }

            /// <summary>
            /// A valid Anomaly/id.
            /// </summary>
            public string Anomaly
            {
                get;
                set;
            }

            /// <summary>
            /// A valid DataSet/id.
            /// </summary>
            public string DataSet
            {
                get;
                set;
            }

            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                if(!string.IsNullOrWhiteSpace(Anomaly)) json.anomaly = Anomaly;
                if (!string.IsNullOrWhiteSpace(DataSet)) json.dataset = DataSet;
                return json;
            }
        }
    }
}