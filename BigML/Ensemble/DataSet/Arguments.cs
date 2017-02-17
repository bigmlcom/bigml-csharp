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
                Seed = "";
                FieldInfos = new Dictionary<string,Field>();
                ExcludedFields = new List<string>();
                OriginDatasets = new List<string>();
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
            /// A valid dataset/id. Used for sample and filter a dataset
            /// </summary>
            public string OriginDataset
            {
                get;
                set;
            }

            /// <summary>
            /// A list of valid dataset/id. Used for create multidataset
            /// </summary>
            public List<string> OriginDatasets
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

            /// <summary>
            /// A list of strings that specifies the fields that won't be
            /// included in the dataset
            /// </summary>
            public List<string> ExcludedFields
            {
                get;
                set;
            }

            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                if (Source != null) {
                    json.source = Source;
                }
                if (OriginDataset != null)
                {
                    json.origin_dataset = OriginDataset;
                }
                if (OriginDatasets.Count > 0)
                {
                    var origin_datasets = new JsonArray();
                    foreach (var oneDataSet in OriginDatasets)
                    {
                        origin_datasets.Add((JsonValue)oneDataSet);
                    }
                    json.origin_datasets = origin_datasets;
                }
                if (!string.IsNullOrEmpty(Seed)) json.seed = Seed;
                if (Size != 0) json.size = Size;
                if (SampleRate != 0.0) json.sample_rate = SampleRate;
                json.out_of_bag = OutOfBag;
                if (ExcludedFields.Count > 0)
                {
                    var excluded_fields = new JsonArray();
                    foreach (var excludedField in ExcludedFields)
                    {
                        excluded_fields.Add((JsonValue) excludedField);
                    }
                    json.excluded_fields = excluded_fields;
                }

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