using System.Collections.Generic;
using System.Json;

namespace BigML
{
    public partial class TopicDistribution
    {
        public class Arguments : Arguments<TopicDistribution>
        {
            public Arguments()
            {
                InputData = new Dictionary<string, object>();
            }

            /// <summary>
            /// An object with field's id/value pairs representing the instance you want to create a prediction for.
            /// </summary>
            public IDictionary<string,object> InputData
            {
                get;
                private set;
            }

            /// <summary>
            /// A valid topicmodel/id.
            /// </summary>
            public string TopicModel
            {
                get;
                set;
            }


            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                if (TopicModel != null)
                {
                    json.topicmodel = TopicModel;
                }

                if (InputData.Count > 0)
                {
                    var input_data = new JsonObject();
                    foreach(var kv in InputData)
                    {
                        JsonPrimitive value;
                        JsonPrimitive.TryCreate(kv.Value, out value);
                        input_data[kv.Key] = value;
                    }
                    json.input_data = input_data;
                }

                return json;
            }
        }
    }
}