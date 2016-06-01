using System.Collections.Generic;
using System.Json;

namespace BigML
{
    public partial class Execution
    {
        public class Arguments : Arguments<Execution>
        {
            public Arguments(Script script): this()
            {
               this.Script = script;
            }

            public Arguments()
            {
               
            }

            /// <summary>
            /// A valid script id
            /// </summary>
            public Script Script
            {
                get;
                set;
            }

            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                return json;
            }
        }
    }
}