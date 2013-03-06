using System.Collections.Generic;
using System.Json;

namespace BigML
{
    public partial class DataSet
    {
        public class Arguments : Arguments<DataSet>
        {
            public Arguments(Source source): this()
            {
                Source = source.Resource;
            }

            public Arguments()
            {
                FieldInfos = new Dictionary<string,Field>();
            }

            /// <summary>
            /// Data for inline source creation. 
            /// </summary>
            public IDictionary<string, Field> FieldInfos 
            {
                get;
                private set;
            }

            /// <summary>
            /// The number of bytes from the source that you want to use.
            /// </summary>
            public int Size
            {
                get;
                set;
            }

            /// <summary>
            /// A valid source/id. 
            /// </summary>
            public string Source
            {
                get;
                set;
            }

            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                json.source = Source;
                if (Size != 0) json.size = Size;
                if(FieldInfos.Count > 0)
                {
                   var field = new JsonObject();
                   foreach(var kv in FieldInfos)
                   {
                       field[kv.Key] = kv.Value.ToJson();
                   }
                   json.field = field;
                } 

                return json;
            }
        }
    }
}