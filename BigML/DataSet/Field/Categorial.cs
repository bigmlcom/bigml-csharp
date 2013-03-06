using System.Collections.Generic;
using System.Json;

namespace BigML
{
    public partial class DataSet
    {
        public partial class Field
        {
            public abstract partial class Summary
            {
                /// <summary>
                /// Categorical summaries give you a count per each category and missing count in case any of the instances contain missing values.
                /// </summary>
                public class Categorical : Summary
                {
                    internal Categorical(JsonValue json)
                        : base(json)
                    {
                    }

                    /// <summary>
                    /// A dictionary where the keys are the unique categories found in the field and the values are the count for that category.
                    /// </summary>
                    public IDictionary<string, int> Counts
                    {
                        get
                        {
                            var counts = new Dictionary<string, int>();
                            foreach (var kv in _summary.counts) counts[kv.Key] = kv.Value;
                            return counts;
                        }
                    }
                }
            }
        }
    }
}
