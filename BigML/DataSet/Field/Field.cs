using System.Collections.Generic;
using Newtonsoft.Json.Linq;
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
            internal Field(JObject json): base(json)
            {
                _field = json;

                MissingTokens = json["missing_tokens"] != null
                                    ? new HashSet<string>(json["missing_tokens"].Select(x => (string)x))
                                    : new HashSet<string>();
            }

            /// <summary>
            /// Public constructor to create fields for use in argument.
            /// </summary>
            public Field(): this(new JObject())
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


            private OpType? _optype;

            /// <summary>
            /// Specifies the type of the field.
            /// It can be numerical, categorical, datatime, text or items.
            /// </summary>
            public OpType Optype
            {
                get {
                    if (_optype == null)
                    {
                        _optype = Constants.getOpType((string)_field.optype);
                    }
                    return (OpType) _optype;
                }
                set { _field.optype = value.ToString().ToLower(); }
            }

            public ISet<string> MissingTokens
            {
                get;
                private set;
            }


            private Summary _fieldSummary;

            /// <summary>
            /// Numeric or categorical summary of the field.
            /// Downcast to appropriate (Summary.Numeric, Summary.Categorical,
            /// Summary.Datetime, Summary.Text, Summary.Items) subtype as needed.
            /// </summary>
            public Summary FieldSummary
            {
                get
                {
                    if (_fieldSummary == null)
                    {
                        switch (Optype)
                        {
                            case OpType.Categorical:
                                _fieldSummary = new Summary.Categorical(_field.summary);
                                break;
                            case OpType.Numeric:
                                _fieldSummary = new Summary.Numeric(_field.summary);
                                break;
                            case OpType.Datetime:
                                _fieldSummary = new Summary.Datetime(_field.datetime);
                                break;
                            case OpType.Text:
                                _fieldSummary = new Summary.Text(_field.summary);
                                break;
                            case OpType.Items:
                                _fieldSummary = new Summary.Items(_field.summary);
                                break;
                            default:
                                _fieldSummary = default(Summary);
                                break;
                        }
                    }
                    return _fieldSummary;
                }
            }

            private Dictionary<string, dynamic> _termAnalysis;

            public Dictionary<string, dynamic> TermAnalysis
            {
                get
                {
                    if (Optype != OpType.Text)
                    {
                        return null;
                    }

                    if (_termAnalysis == null)
                    {
                        _termAnalysis = new Dictionary<string, dynamic>();
                        foreach (var kv in _field.term_analysis)
                        {
                            _termAnalysis.Add(kv.Name, kv.Value);
                        }
                    }
                    return _termAnalysis;
                }
            }


            private Dictionary<string, dynamic> _itemAnalysis;

            public Dictionary<string, dynamic> ItemAnalysis
            {
                get
                {
                    if (this.Optype != OpType.Items)
                    {
                        return null;
                    }

                    if (_itemAnalysis == null) { 
                        _itemAnalysis = new Dictionary<string, dynamic>();
                        foreach (var kv in _field.item_analysis)
                        {
                            _itemAnalysis.Add(kv.Name, kv.Value);
                        }
                    }
                    return _itemAnalysis;
                }
            }


            public JObject ToJson()
            {
                dynamic copy = new JObject();

                if (!string.IsNullOrWhiteSpace(Name)) copy.name = Name;
                if (!string.IsNullOrWhiteSpace(Locale)) copy.locale = Locale;
                if (this.Optype != OpType.Error) copy.optype = OpType.ToString().ToLower();
                if (MissingTokens.Count > 0) copy.missing_tokens = new JArray(MissingTokens.Select(t => (JValue)t));

                return copy;
            }

            public override string ToString()
            {
                return ToJson().ToString();
            }

        }
    }
}