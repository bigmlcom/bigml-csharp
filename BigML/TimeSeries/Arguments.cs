using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class TimeSeries
    {
        public class Arguments : Arguments<TimeSeries>
        {
            public Arguments()
            {
                
            }

            /// <summary>
            /// A valid dataset/id.
            /// </summary>
            public string DataSet
            {
                get;
                set;
            }

            public override JObject ToJson()
            {
                dynamic json = base.ToJson();

                if(!string.IsNullOrWhiteSpace(DataSet)) json.dataset = DataSet;
                return json;
            }
        }
    }
}