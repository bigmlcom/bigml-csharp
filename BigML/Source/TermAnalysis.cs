using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Source
    {
        /// <summary>
        /// Set of parameters about the term analysis of a source.
        /// </summary>
        public class TermAnalysis
        {
            readonly dynamic _termanalysis;

            /// <summary>
            /// Create a new SourceParser object to be passed as an argument to
            /// CreateSource
            /// </summary>
            public TermAnalysis()
                : this(new JObject())
            {
                Enabled = _enabled;
                UseStopwords = _use_stopwords;
                StemWords = _stem_words;
                CaseSensitive = _case_sensitive;
                Language = _language;
                TokenMode = _token_mode;
            }

            internal TermAnalysis(JObject json)
            {
                _termanalysis = json;
            }

            /// <summary>
            /// Whether text processing should be enabled or not.
            /// Default is true.
            /// </summary>
            public bool Enabled
            {
                get { return _termanalysis.enabled; }
                set { _termanalysis.enabled = value; }
            }
            private const bool _enabled = true;


            /// <summary>
            /// Whether to use stop words or not. Default is true.
            /// </summary>
            public bool UseStopwords
            {
                get { return _termanalysis.use_stopwords; }
                set { _termanalysis.use_stopwords = value; }
            }
            private const bool _use_stopwords = true;


            /// <summary>
            /// Whether to stem words or not. Default is true.
            /// </summary>
            public bool StemWords
            {
                get { return _termanalysis.stem_words; }
                set { _termanalysis.stem_words = value; }
            }
            private const bool _stem_words = true;



            /// <summary>
            /// Whether text analysis should be case sensitive or not.
            /// Default is false.
            /// </summary>
            public bool CaseSensitive
            {
                get {
                    return _termanalysis.case_sensitive;
                }
                set { _termanalysis.case_sensitive = value; }
            }
            private const bool _case_sensitive = false;


            /// <summary>
            /// The default language of text fields.
            /// Default is "en".
            /// </summary>
            public string Language
            {
                get
                {
                    var language = _termanalysis.language;

                    return language is JToken ? language : _language;
                }
                set { _termanalysis.language = value; }
            }
            private const string _language = "en";


            /// <summary>
            /// Whether "tokens_only", "full_terms_only" or "all" should be
            /// tokenized. Default is "all".
            /// </summary>
            public string TokenMode
            {
                get
                {
                    var token_mode = _termanalysis.token_mode;

                    return token_mode is JToken ? token_mode : _token_mode;
                }
                set { _termanalysis.token_mode = value; }
            }
            private const string _token_mode = "all";


            public JObject ToJson()
            {
                dynamic copy = new JObject();

                if (Enabled != _enabled) copy.enabled = Enabled;
                if (UseStopwords != _use_stopwords) copy.use_stopwords = UseStopwords;
                if (StemWords != _stem_words) copy.stem_words = StemWords;
                if (CaseSensitive != _case_sensitive) copy.case_sensitive = CaseSensitive;
                if (Language != _language) copy.language = Language;
                if (TokenMode != _token_mode) copy.token_mode = TokenMode;

                return copy;
            }

            public override string ToString()
            {
                return ToJson().ToString();
            }
        }
    }
}
