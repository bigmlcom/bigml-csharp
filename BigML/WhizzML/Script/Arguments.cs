using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Script
    {
        public class Arguments : Arguments<Script>
        {
            public Arguments(string sourceCode): this()
            {
               this.SourceCode = sourceCode;
            }

            public Arguments()
            {
                Imports = new List<string>();
                Inputs = new JArray();
            }

            /// <summary>
            /// A list of valid library/id.
            /// Used to include declared methods in a script
            /// </summary>
            public List<string> Imports
            {
                get;
                set;
            }

            /// <summary>
            /// A list of valid inputs definitions
            /// </summary>
            public JArray Inputs
            {
                get;
                set;
            }

            /// <summary>
            /// A valid source code
            /// </summary>
            public string SourceCode
            {
                get;
                set;
            }

            public override JObject ToJson()
            {
                dynamic json = base.ToJson();

                if (Imports.Count > 0)
                {
                    var imports = new JArray();
                    foreach (var oneLibrary in Imports)
                    {
                        imports.Add((JValue)oneLibrary);
                    }
                    json.imports = imports;
                }


                if (Inputs.Count > 0)
                {
                    json.inputs = new JArray();
                    foreach (var parameter in Inputs)
                    {
                        json.inputs.Add(parameter);
                    }
                }
                return json;
            }
        }
    }
}