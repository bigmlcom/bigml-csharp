using System.Collections.Generic;
using Newtonsoft.Json.Linq;

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
                    internal Items(JObject json)
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
                    /// A list of pairs where the first one is an item and the second is the number of times that appears in the dataset
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
