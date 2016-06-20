using System.Collections.Generic;
using System.Json;
using System.Linq;

namespace BigML
{
    public partial class DataSet
    {
        /// <summary>
        /// The column number, the name of the field, the type of the field, and the summary of a dataset field.
        /// </summary>
        public partial class Field : Field<DataSet>
        {
            readonly dynamic _field;

            /// <summary>
            /// Internal constructor for use when created internally.
            /// </summary>
            internal Field(JsonValue json): base(json)
            {
                _field = json;
              
                MissingTokens = json.ContainsKey("missing_tokens")
                                    ? new HashSet<string>(json["missing_tokens"].Select(x => (string)x))
                                    : new HashSet<string>();
            }

            /// <summary>
            /// Public constructor to create fields for use in argument.
            /// </summary>
            public Field(): this(new JsonObject())
            {
            }

            /// <summary>
            /// Name of the field. 
            /// </summary>
            public new string Name
            {
                get { return base.Name; }
            }

            /// <summary>
            /// id of the field. 
            /// </summary>
            public new string Id
            {
                get { return base.Id; }
            }

            /// <summary>
            /// The specific locale for this field. 
            /// </summary>
            public string Locale
            {
                get { return _field.locale; }
            }

            /// <summary>
            /// Specifies the type of the field. 
            /// It can be numerical, categorical, datatime, text or items.
            /// </summary>
            public new OpType OpType
            {
                get { return Constants.getOpType((string) _field.optype); }
                set { _field.optype = value.ToString().ToLower(); }
            }

            public ISet<string> MissingTokens
            {
                get; 
                private set;
            }

            /// <summary>
            /// Numeric or categorical summary of the field.
            /// Downcast to appropriate (Summary.Numeric, Summary.Categorical,
            /// Summary.Datetime, Summary.Text, Summary.Items) subtype as needed.
            /// </summary>
            public Summary FieldSummary
            {
                get
                {
                    switch(OpType)
                    {
                        case OpType.Categorical:
                            return new Summary.Categorical(_field.summary);
                        case OpType.Numeric:
                            return new Summary.Numeric(_field.summary);
                        case OpType.Datetime:
                            return new Summary.Datetime(_field.datetime);
                        case OpType.Text:
                            return new Summary.Text(_field.summary);
                        case OpType.Items:
                            return new Summary.Items(_field.summary);
                        default:
                            return default(Summary);
                    }
                }
            }


            public Dictionary<string, dynamic> TermAnalysis
            {
                get
                {
                    if (OpType != OpType.Text)
                    {
                        return null;
                    }

                    var _termAnalysis = new Dictionary<string, dynamic>();
                    foreach (var kv in _field.term_analysis)
                    {
                        _termAnalysis.Add(kv.Key, kv.Value);
                    }
                    return _termAnalysis;
                }
            }



            public JsonValue ToJson()
            {
                dynamic copy = new JsonObject();

                if (!string.IsNullOrWhiteSpace(Name)) copy.name = Name;
                if (!string.IsNullOrWhiteSpace(Locale)) copy.locale = Locale;
                if (OpType != OpType.Error) copy.optype = OpType.ToString().ToLower();
                if (MissingTokens.Count > 0) copy.missing_tokens = new JsonArray(MissingTokens.Select(t => (JsonValue)t));

                return copy;
            }

            public override string ToString()
            {
                return ToJson().ToString();
            }

        }
    }
}