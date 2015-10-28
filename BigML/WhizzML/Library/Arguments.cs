using System.Collections.Generic;
using System.Json;

namespace BigML
{
    public partial class Library
    {
        public class Arguments : Arguments<Library>
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