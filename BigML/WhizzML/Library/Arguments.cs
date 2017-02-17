using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Library
    {
        public class Arguments : Arguments<Library>
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