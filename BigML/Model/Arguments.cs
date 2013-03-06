using System.Collections.Generic;
using System.Json;
using System.Linq;

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
            /// Specifies the percentage of instances that you do not want to be considered directly when building the model 
            /// but to use them to later prune the model and obtain and model that generalizes better. 
            /// </summary>
            public double HoldOut
            {
                get;
                set;
            }

            /// <summary>
            /// Specifies the ids of the fields that you want to use as predictors to create the model. 
            /// TODO use Field?
            /// </summary>
            public ICollection<string> Inputs
            {
                get;
                private set;
            }

            /// <summary>
            /// Specifies the id of the field that you want to predict.
            /// TODO use Field? 
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

            public override JsonValue ToJson()
            {
                dynamic json = base.ToJson();

                if(!string.IsNullOrWhiteSpace(DataSet)) json.dataset = DataSet;
                if (!double.IsNaN(HoldOut)) json.holdout = HoldOut;
                if(Inputs.Count > 0) json.input_fields = new JsonArray(Inputs.Select(input => (JsonValue)input));
                if (!string.IsNullOrWhiteSpace(Objective)) json.Objective = new JsonArray((JsonValue)Objective);
                if(Range.Count >= 2) json.range = new JsonArray(Range.Take(2).Select(index => (JsonValue)index));
                
                return json;
            }
        }
    }
}