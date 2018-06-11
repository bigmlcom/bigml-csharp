using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Source
    {
        /// <summary>
        /// Set of parameters to parse a source. 
        /// </summary>
        public class Parser
        {
            readonly dynamic _sourceparser;

            /// <summary>
            /// Create a new SourceParser object to be passed as an argument to CreateSource
            /// </summary>
            public Parser()
                : this(new JObject())
            {
                Header = _header;
                Trim = _trim;
                Separator = _separator;
                Quote = _quote;
                Locale = _locale;
            }

            internal Parser(JObject json)
            {
                _sourceparser = json;
                MissingTokens = json["missing_tokens"] != null
                                    ? new HashSet<string>(json["missing_tokens"].Select(x => (string) x))
                                    : new HashSet<string>();
            }

            /// <summary>
            /// Whether the source contains a header. Default is true.
            /// </summary>
            public bool Header
            {
                get { return _sourceparser.header; }
                set { _sourceparser.header = value; }
            }
            private const bool _header = true;

            /// <summary>
            /// Whether to trim field strings. 
            /// Default is true.
            /// </summary>
            public bool Trim
            {
                get
                {
                    var trim = _sourceparser.trim;
                    return trim is JToken ? trim : _trim;
                }
                set { _sourceparser.trim = value; }
            }
            private const bool _trim = true;

            /// <summary>
            /// The source separator character. 
            /// Default is ','.
            /// </summary>
            public string Separator
            {
                get
                {
                    var separator = _sourceparser.separator;
                    return separator is JToken ? separator : _separator;
                }
                set { _sourceparser.separator = value; }
            }
            private const string _separator = ",";

            /// <summary>
            /// The source quote character. 
            /// Default is '"'.
            /// </summary>
            public string Quote
            {
                get
                {
                    var quote = _sourceparser.quote;
                    return quote is JToken ? quote : _quote;
                }
                set { _sourceparser.quote = value; }
            }
            private const string _quote = "\"";

            /// <summary>
            /// The locale of the source. 
            /// Default is "en-US".
            /// </summary>
            public string Locale
            {
                get
                {
                    var locale = _sourceparser.locale;
                    return locale is JToken ? locale : _locale;
                }
                set { _sourceparser.locale = value; }
            }
            private const string _locale = "en-US";

            /// <summary>
            /// Tokens that represent a missing value. 
            /// Default is { "", N/A, n/a, NULL, null, -, #DIV/0, #REF!, #NAME?, NIL, nil, NA, na, #VALUE!, #NULL!, NaN, #N/A, #NUM!, ?}.
            /// </summary>
            public ISet<string> MissingTokens { get; private set; }

            public JObject ToJson()
            {
                dynamic copy = new JObject();

                if (Header != _header) copy.header = Header;
                if (Trim != _trim) copy.trim = Trim;
                if (Separator != _separator) copy.separator = Separator;
                if (Quote != _quote) copy.quote = Quote;
                if (Locale != _locale) copy.locale = Locale;
                if (MissingTokens.Count > 0) copy.missing_tokens = new JArray(MissingTokens.Select(t => (JValue) t));

                return copy;
            }

            public bool isEmpty()
            {

                if (Header != _header) return false;
                if (Trim != _trim) return false;
                if (Separator != _separator) return false;
                if (Quote != _quote) return false;
                if (Locale != _locale) return false;
                if (MissingTokens.Count > 0) return false;

                return true;
            }

            public override string ToString()
            {
                return ToJson().ToString();
            }
        }
    }
}