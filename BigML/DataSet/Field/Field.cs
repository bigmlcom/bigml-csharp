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
            /// It can be numerical, categorical, or text.
            /// </summary>
            public new OpType OpType
            {
                get { return base.OpType; }
                set { _field.optype = value.ToString().ToLower(); }
            }

            public ISet<string> MissingTokens
            {
                get; 
                private set;
            }

            /// <summary>
            /// Numeric or categorical summary of the field.
            /// Downcast to appropriate (Summary.Numeric or Summary.Categorical) subtype as needed.
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
                        default:
                            return default(Summary);
                    }
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