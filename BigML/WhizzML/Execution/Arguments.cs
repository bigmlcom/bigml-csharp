using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Execution
    {
        public class Arguments : Arguments<Execution>
        {
            public Arguments(Script script): this()
            {
               this.Script = script.Resource;
            }

            public Arguments()
            {
                Inputs = new List<dynamic[]>();
            }

            /// <summary>
            /// A valid script id
            /// </summary>
            public string Script
            {
                get;
                set;
            }

            /// <summary>
            /// Pairs of name and value for the script parameters.
            /// </summary>
            public List<dynamic[]> Inputs
            {
                get;
                set;
            }

            public override JObject ToJson()
            {
                dynamic json = base.ToJson();

                if (Script != null)
                {
                    json.script = Script;
                }

                if (Inputs.Count > 0)
                {
                    json.inputs = new JArray();
                    JArray oneParam;
                    foreach (var parameter in Inputs)
                    {
                        oneParam = new JArray();
                        oneParam.Add(parameter[0]);
                        oneParam.Add(parameter[1]);
                        json.inputs.Add(oneParam);
                    }
                }

                return json;
            }
        }
    }
}