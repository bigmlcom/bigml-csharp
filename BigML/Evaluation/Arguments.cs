using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace BigML
{
    public partial class Evaluation
    {
        public class Arguments : Arguments<Evaluation>
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

            /// <summary>
            /// A valid model/id.
            /// </summary>
            public string Model
            {
                get;
                set;
            }

            /// <summary>
            /// A valid ensemble/id.
            /// </summary>
            public string Ensemble
            {
                get;
                set;
            }


            public override JObject ToJson()
            {
                dynamic json = base.ToJson();

                if(!string.IsNullOrWhiteSpace(DataSet)) json.dataset = DataSet;
                if (!string.IsNullOrWhiteSpace(Model)) json.model = Model;
                if (!string.IsNullOrWhiteSpace(Ensemble)) json.ensemble = Ensemble;
                return json;
            }
        }
    }
}