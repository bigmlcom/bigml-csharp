using System.Collections.Generic;
using System.Json;
using System.Linq;

namespace BigML
{
    public partial class Project
    {
        public class Arguments : Arguments<Project>
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