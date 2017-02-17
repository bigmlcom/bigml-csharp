using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Model
    {
        public class Arguments : Arguments<Model>
        {
            public Arguments()
            {
                Inputs = new List<string>();
                Range = new List<int>(2);
                ExcludedFields = new List<string>();
            }

            /// <summary>
            /// A valid dataset/id.
            /// </summary>
            public string DataSet
            {
                get;
                set;
            }

            /// <summary>
            /// Specifies the ids of the fields that you want to use as predictors to create
            /// the model.
            /// TODO use Field?
            /// </summary>
            public ICollection<string> Inputs
            {
                get;
                private set;
            }

            /// <summary>
            /// Specifies the id of the field that you want to predict with this model.
            /// </summary>
            public string Objective
            {
                get; 
                set;
            }

            /// <summary>
            /// The range of successive instances to build the model. 
            /// </summary>
            public ICollection<int> Range
            {
                get;
                private set;
            }

            /// <summary>
            /// A list of strings that specifies the fields that won't be
            /// included in the model
            /// </summary>
            public List<string> ExcludedFields
            {
                get;
                set;
            }

            public override JObject ToJson()
            {
                dynamic json = base.ToJson();

                if(!string.IsNullOrWhiteSpace(DataSet)) json.dataset = DataSet;
                if(Inputs.Count > 0)
                    json.input_fields = new JArray(Inputs.Select(input => (JValue)input));
                if (!string.IsNullOrWhiteSpace(Objective))
                    json.Objective = new JArray((JValue) Objective);
                if(Range.Count >= 2)
                    json.range = new JArray(Range.Take(2).Select(index => (JValue)index));
                if (ExcludedFields.Count > 0)
                {
                    var excluded_fields = new JArray();
                    foreach (var excludedField in ExcludedFields)
                    {
                        excluded_fields.Add((JValue)excludedField);
                    }
                    json.excluded_fields = excluded_fields;
                }

                return json;
            }
        }
    }
}