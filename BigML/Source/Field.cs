using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Source
    {
        public class Field : Field<Source>
        {
            internal Field(JObject json): base(json)
            {
            }
        }

    }
}