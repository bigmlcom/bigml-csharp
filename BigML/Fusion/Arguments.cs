using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

namespace BigML
{
    public partial class Fusion
    {
        public class Arguments : Arguments<Fusion>
        {
            public Arguments()
            {

            }

            /// <summary>
            /// A list of model IDs used to create the fusion or a list of
            /// objects including model propperties like the weight
            /// </summary>
            public List<dynamic> Models
            {
                get;
                set;
            }


            public override JObject ToJson()
            {
                dynamic json = base.ToJson();

                if (Models.Count > 0) {
                    json.models = new JArray(Models.Select(t => (JValue)t));
                }
                return json;
            }
        }
    }
}