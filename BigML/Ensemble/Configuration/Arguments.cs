using System.Collections.Generic;
using System.Json;
using System.Linq;

namespace BigML
{
    public partial class Configuration
    {
        public class Arguments : Arguments<Configuration>
        {
            public Arguments()
            {

            }

            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                return json;
            }
        }
    }
}