using System.Collections.Generic;
using Newtonsoft.Json.Linq;

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
            /// An object with field's id/value pairs representing the instance
            /// you want to create a prediction for.
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


            public override JObject ToJson()
            {
                dynamic json = base.ToJson();

                if (TopicModel != null)
                {
                    json.topicmodel = TopicModel;
                }

                if (InputData.Count > 0)
                {
                    var input_data = new JObject();
                    foreach(var kv in InputData)
                    {
                        if (kv.Value.GetType().isNumericType())
                        {
                            input_data[kv.Key] = (double)kv.Value;
                        }
                        else
                        {
                            input_data[kv.Key] = (string)kv.Value;
                        }
                    }
                    json.input_data = input_data;
                }

                return json;
            }
        }
    }
}