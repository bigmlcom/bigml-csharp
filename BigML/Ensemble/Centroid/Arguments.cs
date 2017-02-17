using System.Collections.Generic;
using System.Json;
using System.Linq;

namespace BigML
{
    public partial class Centroid
    {
        public class Arguments : Arguments<Centroid>
        {

            /// <summary>
            /// A valid dataset/id.
            /// </summary>
            public string DataSet
            {
                get;
                set;
            }


            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                if(!string.IsNullOrWhiteSpace(DataSet)) json.dataset = DataSet;
                return json;
            }
        }
    }
}