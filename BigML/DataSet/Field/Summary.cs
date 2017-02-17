using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class DataSet
    {
        public partial class Field 
        {
            /// <summary>
            /// Statistical summary of field 
            /// </summary>
            public abstract partial class Summary
            {
                protected readonly dynamic _summary;

                protected Summary(JObject json)
                {
                    _summary = json;
                }


                /// <summary>
                /// Number of instances missing this field.
                /// </summary>
                public int Missing
                {
                    get { return _summary.missing_count; }
                }
            }
        }
    }
}