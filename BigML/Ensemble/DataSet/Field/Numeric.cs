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
                /// Numeric summaries come with all the fields described below. 
                /// Depending on whether the number of different values for the field is lower than 32 or not, 
                /// you can get either counts or splits points.
                /// </summary>
                public class Numeric : Summary
                {
                    internal Numeric(JsonValue json)
                        : base(json)
                    {
                    }

                    /// <summary>
                    /// A dictionary where each key is one of the unique values found in the field and the value is the count. 
                    /// Only available when the number of distinct values is less than or equal to 32.
                    /// </summary>
                    public IDictionary<int, int> Counts
                    {
                        get
                        {
                            var counts = new Dictionary<int, int>();
                            foreach (var kv in _summary.counts) counts[(int)kv[0]] = (int)kv[1];
                            return counts;
                        }
                    }

                    /// <summary>
                    /// The maximum value found in this field.
                    /// </summary>
                    public int Maximum
                    {
                        get { return _summary.maximum; }
                    }

                    /// <summary>
                    /// The median value found in this field.
                    /// </summary>
                    public int Median
                    {
                        get { return _summary.median; }
                    }

                    /// <summary>
                    /// The minimum value found in this field.
                    /// </summary>
                    public int Minimum
                    {
                        get { return _summary.minimum; }
                    }

                    /// <summary>
                    /// The number of instances containing data for this field.
                    /// </summary>
                    public int Population
                    {
                        get { return _summary.population; }
                    }

                    /// <summary>
                    /// Histogram split points for this field. 
                    /// The split points break the distribution into buckets of equal population. 
                    /// This helps you easily compute histograms with you preferred number of bins. 
                    /// Only available when the number of distinct values is more than 32.
                    /// </summary>
                    public IEnumerable<double> Splits
                    {
                        get { return (_summary.splits as JsonArray).Select(s => (double)s); }
                    }

                    /// <summary>
                    /// Sum of all values for this field (for mean calculation).
                    /// </summary>
                    public double Sum
                    {
                        get { return _summary.sum; }
                    }

                    /// <summary>
                    /// Sum of squared values (for variance calculation).
                    /// </summary>
                    public double SumSquares
                    {
                        get { return _summary.sum_squares; }
                    }
                }
            }
        }
    }

    
}
