using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Text;

namespace BigML
{
    public static class Utils
    {

        // Headers
        internal static string JSON = "application/json; charset=utf-8";
        
        /// <summary>
        /// Returns True if value is a valid URL.
        /// 
        /// </summary>
        /*public static bool isUrl(string value)
        {
            try
            {
                new URL(value);
                return true;
            }
            catch (MalformedURLException)
            {
                return false;
            }
        }*/


        /// <summary>
        /// Computes the mean of a distribution in the [[point, instances]] syntax
        /// </summary>
        /// <param name="distribution">
        /// @return </param>
        public static double meanOfDistribution(IList<JArray> distribution)
        {
            double addition = 0.0f;
            long count = 0;

            foreach (JArray bin in distribution)
            {
                double point = (double) bin[0];
                long instances = (long) bin[1];

                addition += point * instances;
                count += instances;
            }

            if (count > 0)
            {
                return addition / count;
            }

            return Double.NaN;
        }

        /// <summary>
        /// Computes the mean of a list of double values
        /// </summary>
        public static double meanOfValues(IList<double?> values)
        {
            double addition = 0.0f;
            long count = values.Count;

            foreach (double? value in values)
            {
                addition += value.Value;
            }

            return addition / count;
        }


        /// <summary>
        /// Prints distribution data
        /// </summary>
        public static StringBuilder printDistribution(JArray distribution)
        {
            StringBuilder distributionStr = new StringBuilder();

            int total = 0;
            foreach (object binInfo in distribution)
            {
                JArray binInfoArr = (JArray) binInfo;
                total += ((int) binInfoArr[1]);
            }

            foreach (object binInfo in distribution)
            {
                JArray binInfoArr = (JArray)binInfo;
                distributionStr.Append(string.Format("    {0}: {1:F2}% ({2:D} instance{3})\n", binInfoArr[0], Utils.roundOff((float)(((int)binInfoArr[1]) * 1.0 / total), 4) * 100, binInfoArr[1], (((int)binInfoArr[1]) == 1 ? "" : "s")));
            }


            return distributionStr;
        }

        /// <summary>
        /// Adds up a new distribution structure to a map formatted distribution
        /// </summary>
        /// <param name="distribution"> </param>
        /// <param name="newDistribution">
        /// @return </param>
        public static Dictionary<object, double> mergeDistributions(Dictionary<object, double> distribution, Dictionary<object, double> newDistribution)
        {
            foreach (object value in newDistribution.Keys)
            {
                if (!distribution.ContainsKey(value))
                {
                    distribution[value] = 0;
                }
                distribution[value] = distribution[value] + newDistribution[value];
            }

            return distribution;
        }


        /// <summary>
        /// We switch the Array to a Map structure in order to be more easily manipulated
        /// </summary>
        /// <param name="distribution"> current distribution as an JsonArray instance </param>
        /// <returns> the distribution as a Map instance </returns>

        public static Dictionary<object, double> convertDistributionArrayToMap(JArray distribution)
        {
            Dictionary<object, double> newDistribution = new Dictionary<object, double>();
            foreach (object distValueObj in distribution)
            {
                JArray distValueArr = (JArray)distValueObj;
                newDistribution[distValueArr[0]] = (double)distValueArr[1];
            }

            return newDistribution;
        }


        public static bool isNumericType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Round a double number x to n decimal places
        /// </summary>
        public static double roundOff(double x, int n)
        {
            double bd = Math.Round((double)x, n, MidpointRounding.ToEven);
            return bd;
        }

        /// <summary>
        /// We switch the Array to a Map structure in order to be more easily manipulated
        /// </summary>
        /// <param name="distribution"> current distribution as an JsonArray instance </param>
        /// <returns> the distribution as a Map instance </returns>

        public static JArray convertDistributionMapToSortedArray(IDictionary<object, double> distribution)
        {
            JArray newDistribution = new JArray();

            string opType = BigML.OpType.Numeric.ToString();

            foreach (object key in distribution.Keys)
            {
                JArray element = new JArray();
                element.Add((JValue) key);
                element.Add(distribution[key]);
                newDistribution.Add(element);

                if (isNumericType(key))
                {
                    opType = BigML.OpType.Numeric.ToString();
                }
                else if (key is string)
                {
                    opType = BigML.OpType.Text.ToString();
                }
            }

            /*
            if (distribution != null && distribution.Count > 0)
            {

                //TODO: SORT newDistribution
                foreach (JsonValue element in newDistribution)
                {
                    foreach (JsonValue element1 in newDistribution)
                    {
                        if (element == element1)
                        {
                            continue;
                        }

                        if (OpType.Numeric.Equals(opType))
                        {
                            if ( ((double) element[0]) > ((double) element1[0]) )
                            {

                            }
                        }
                        else if (OpType.Text.Equals(opType))
                        {
                            if ( ((double)element[0]) > ((double)element1[0]) )
                            {

                            }
                        }
                        else
                        { // OPTYPE_DATETIME
                          // TODO: implement this
                            throw new Exception("Not supported");
                        }
                    }
                }
            }*/

            return newDistribution;
        }


        /// <summary>
        /// Merges the bins of a regression distribution to the given limit number
        /// </summary>
        public static JArray mergeBins(JArray distribution, int limit)
        {
            int length = distribution.Count;
            if (limit < 1 || length <= limit || length < 2)
            {
                return distribution;
            }

            int indexToMerge = 2;
            double shortest = double.MaxValue;
            for (int index = 1; index < length; index++)
            {
                double distance = ((double)((JArray)distribution[index]) [0]) - ((double)((JArray) distribution[index - 1])[0]);

                if (distance < shortest)
                {
                    shortest = distance;
                    indexToMerge = index;
                }
            }

            JArray newDistribution = new JArray();
            for (int index = 0; index < indexToMerge - 1; index++)
            {
                newDistribution.Add(distribution[index]);
            }
            //newDistribution.addAll(distribution.subList(0, indexToMerge - 1));

            JArray left = (JArray)distribution[indexToMerge - 1];

            JArray right = (JArray)distribution[indexToMerge];

            JArray newBin = new JArray();
            newBin.Add((JValue) (((((double)left[0]) * ((double)left[1])) + (((double)right[0]) * ((double)right[1]))) / (((double)left[1]) + ((double)right[1])))); //  position 0
            newBin.Add((JValue) ((double) left[1] + ((double)right[1] ))); // position  1


            newDistribution.Add(newBin);

            if (indexToMerge < (length - 1))
            {
                for (int index = indexToMerge + 1; index < distribution.Count; index++)
                {
                    newDistribution.Add(distribution[index]);
                }
                //newDistribution.addAll(distribution.subList(indexToMerge + 1, distribution.Count));
            }

            return mergeBins(newDistribution, limit);
        }

        /// <summary>
        /// Merges the bins of a regression distribution to the given limit number
        /// </summary>
        public static Dictionary<object, double> mergeBins(IDictionary<object, double> distribution, int limit)
        {
            JArray mergedDist = mergeBins(convertDistributionMapToSortedArray(distribution), limit);
            return convertDistributionArrayToMap(mergedDist);
        }

        /// <summary>
        /// Determines if the given collection contain the same value.
        /// 
        /// We will use the contains method of the list to check if the value is inside
        /// </summary>
        /// <param name="collection"> the list of elements </param>
        /// <param name="value"> the value to check. </param>
        /// <returns> true if all the elements are equals to value or false in otherwise </returns>
        public static bool sameElement(IList collection, object value)
        {
            if (collection == null || collection.Count == 0)
            {
                return false;
            }

            // Iterate over the elements of the collection.
            foreach (object collectionValue in collection)
            {
                // Check if the element in the collection is the same of value
                if (!collectionValue.Equals(value))
                {
                    return false;
                }
            }

            return true;
        }

    }

}