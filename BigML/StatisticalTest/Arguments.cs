using System.Collections.Generic;
using System.Json;
using System.Linq;

namespace BigML
{
    public partial class StatisticalTest
    {
        public class Arguments : Arguments<StatisticalTest>
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


            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                if (!string.IsNullOrWhiteSpace(DataSet)) json.dataset = DataSet;
                return json;
            }
        }
    }
}