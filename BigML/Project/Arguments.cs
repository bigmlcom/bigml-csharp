using System.Collections.Generic;
using Newtonsoft.Json.Linq;
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

            public override JObject ToJson()
            {
                dynamic json = base.ToJson();
                return json;
            }
        }
    }
}