using System.Collections.Generic;
using System.Collections;
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
                /// Text summaries give you a count per each category and missing count in case any of the instances contain missing values.
                /// </summary>
                public class Text : Summary
                {
                    internal Text(JsonValue json)
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
                    public List<dynamic[]> TagCloud
                    {
                        get
                        {
                            var tagCloud = new List<dynamic[]>();
                            foreach (var l in _summary.tag_cloud)
                            {
                                tagCloud.Add(new dynamic[]{l[0], l[1]});
                            }
                            return tagCloud;
                        }
                    }

                    /// <summary>
                    /// A dictionary where the keys are the unique categories found in the field and the values are the count for that category.
                    /// </summary>
                    public Dictionary<string, string[]> TermForms
                    {
                        get
                        {
                            var termForms = new Dictionary<string, string[]>();
                            ArrayList list;
                            foreach (var kv in _summary.term_forms)
                            {
                                list = new ArrayList();
                                foreach (string term in kv.Value)
                                {
                                    list.Add(term);
                                }
                                termForms[kv.Key] = (string[]) list.ToArray("String".GetType());
                            }
                            return termForms;
                        }
                    }
                }
            }
        }
    }
}
