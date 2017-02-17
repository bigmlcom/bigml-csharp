using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class LogisticRegression
    {
        public class Arguments : Arguments<LogisticRegression>
        {
            public Arguments()
            {
                ExcludedFields = new List<string>();
                InputFields = new List<string>();
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
            /// A list of strings that specifies the fields that won't be
            /// included in the logistic regression
            /// </summary>
            public List<string> ExcludedFields
            {
                get;
                set;
            }

            /// <summary>
            /// A list of strings that specifies the fields that will be
            /// included in the logistic regression
            /// </summary>
            public List<string> InputFields
            {
                get;
                set;
            }

            public override JObject ToJson()
            {
                dynamic json = base.ToJson();

                if(!string.IsNullOrWhiteSpace(DataSet)) json.dataset = DataSet;
                if (ExcludedFields.Count > 0)
                {
                    var excluded_fields = new JArray();
                    foreach (var excludedField in ExcludedFields)
                    {
                        excluded_fields.Add(excludedField);
                    }
                    json.excluded_fields = excluded_fields;
                }
                if (InputFields.Count > 0)
                {
                    var input_fields = new JArray();
                    foreach (var inField in InputFields)
                    {
                        input_fields.Add(inField);
                    }
                    json.input_fields = input_fields;
                }
                return json;
            }
        }
    }
}