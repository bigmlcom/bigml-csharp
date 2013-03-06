using System.Json;

namespace BigML
{
    public partial class Source
    {
        public class Field : Field<Source>
        {
            internal Field(JsonValue json): base(json)
            {
            }
        }
        
    }
}