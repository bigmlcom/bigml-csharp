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
                /// Items summaries give you a count per each category and missing count in case any of the instances contain missing values.
                /// </summary>
                public class Items : Summary
                {
                    internal Items(JsonValue json)
                        : base(json)
                    {
                    }

                    public double AverageLength
                    {
                        get
                        {
                            return _summary.average_length;
                        }
                    }

                    /// <summary>
                    /// A dictionary where the keys are the unique categories found in the field and the values are the count for that category.
                    /// </summary>
                    public List<dynamic[]> ItemsCount
                    {
                        get
                        {
                            var items = new List<dynamic[]>();
                            foreach (var l in _summary.items)
                            {
                                items.Add(new dynamic[] { l[0], l[1] });
                            }
                            return items;
                        }
                    }
                }
            }
        }
    }
}
