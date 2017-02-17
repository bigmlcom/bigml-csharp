using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace BigML
{
    public static class Extensions
    {

        public static IEnumerable<T> Select<T>(this JValue source, Func<JValue, T> selector)
        {
            return (source as IEnumerable<JValue> ?? new JValue[]{}).Select(selector);
        }

        /// <summary>
        /// Convert OpType to System.Type
        /// </summary>
        public static Type TypeOf(this OpType optype)
        {
            switch (optype)
            {
                case OpType.Numeric:
                    return typeof (double);
                case OpType.Categorical:
                    return typeof (string);
                case OpType.Text:
                    return typeof (string);
                default:
                    throw new ArgumentOutOfRangeException(string.Format("Cannot convert {0} to System.Type", optype));
            }
        }

        /// <summary>
        /// Get friendly names for field numbers.
        /// </summary>
        public static IDictionary<string, string> FieldNumbersToNames(this DataSet dataset)
        {
            return dataset.Fields.FieldNumbersToNames();
        }

        /// <summary>
        /// Get friendly names for field numbers.
        /// </summary>
        public static IDictionary<string, string> FieldNumbersToNames(this IDictionary<string, DataSet.Field> fieldInfos)
        {
            var names = new Dictionary<string, string>();
            foreach(var number in fieldInfos.Keys)
            {
                names[fieldInfos[number].Name] = number;
            }
            return names;
        }
    }
}