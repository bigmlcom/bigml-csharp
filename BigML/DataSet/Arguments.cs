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
                Seed = "";
                FieldInfos = new Dictionary<string, Field>();
                Source = source.Resource;
            }

            public Arguments()
            {
                Seed = "";
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

            /// <summary>
            /// A valid dataset/id.
            /// </summary>
            public string OriginDataset
            {
                get;
                set;
            }

            /// <summary>
            /// get the rows in the bag or not.
            /// </summary>
            public bool OutOfBag
            {
                get;
                set;
            }

            /// <summary>
            /// A valid float number between zero and one.
            /// </summary>
            public float SampleRate
            {
                get;
                set;
            }

            /// <summary>
            /// A string to generate deterministic samples.
            /// </summary>
            public string Seed
            {
                get;
                set;
            }


            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                json.source = Source;
                if (OriginDataset != null)
                {
                    json.origin_dataset = OriginDataset;
                }
                if (Seed != "") json.seed = Seed;
                if (Size != 0) json.size = Size;
                if (SampleRate != 0.0) json.sample_rate = SampleRate;
                json.out_of_bag = OutOfBag;
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