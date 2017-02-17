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

                return json;
            }
        }
    }
}