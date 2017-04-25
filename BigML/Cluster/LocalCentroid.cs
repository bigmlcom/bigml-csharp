using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Cluster
    {
        /// <summary>
        /// This module defines an auxiliary Centroid predicate structure that
        /// is used in the cluster.
        /// </summary>
        public class LocalCentroid
        {
            public String centroidId;
            public String centroidName;
            public double? distance;
            public int count;

            Dictionary<string, dynamic> _distance;
            Dictionary<string, dynamic> _center;

            public LocalCentroid()
            {
            }

            public LocalCentroid(JObject _jObject)
            {
                centroidId = (string) _jObject["id"];
                centroidName = (string)_jObject["name"];
                _center = _jObject["center"].ToObject<Dictionary<string, dynamic>>();
                _distance = _jObject["distance"].ToObject<Dictionary<string, dynamic>>();
            }

            public double? distance2(Dictionary<string, dynamic> inputData,
                    Dictionary<string, string[]> termSets,
                    Dictionary<string, double> scales,
                    double? stopDistance2 = null)
            {
                double distance2 = 0.0;
                foreach (KeyValuePair<string, dynamic> fieldData in _center)
                {
                    string fieldId = fieldData.Key;
                    dynamic value = fieldData.Value;
                    string[] terms;
                    string typeName = value.GetType().Name;

                    double fScale = scales[fieldId];
                    if (typeName == "JArray"){
                        // text field
                        value = value.ToObject<string[]>();
                        if (!termSets.ContainsKey(fieldId))
                        {
                            terms = new string[] { };
                        }
                        else
                        {
                            terms = termSets[fieldId];
                        }
                        distance2 += cosineDistance2(terms, value, fScale);
                        //System.Console.WriteLine("with field " + fieldId + "(Ar) d2 is " + distance2);
                    }
                    else if(typeName == "String")
                    {
                        // categorical field
                        value = (string) value;
                        if (!inputData.ContainsKey(fieldId) || inputData[fieldId] != value)
                        {
                            distance2 += 1 * Math.Pow(fScale, 2);
                        }
                        //System.Console.WriteLine("with field " + fieldId + "(st) d2 is " + distance2);
                    }
                    else {
                        // numeric field
                        distance2 += Math.Pow(((inputData[fieldId] - value) * fScale), 2);
                        //System.Console.WriteLine("with field " + fieldId + "(nu) d2 is " + distance2);
                    }

                    // if this centroid is farer than previous one => stop
                    if (stopDistance2 != null && distance2 >= stopDistance2)
                    {
                        return null;
                    }
                }
                return distance2;
            }


            /// <summary>
            /// Returns the distance defined by cosine similarity
            /// </summary>
            /// <param name="terms"></param>
            /// <param name="centroidTerms"></param>
            /// <param name="scale"></param>
            /// <returns></returns>
            double cosineDistance2(string[] terms, string[] centroidTerms, double scale) {
                int termsLen = terms.Length;
                int cenTermsL = centroidTerms.Length;

                // Centroid values for the field can be an empty list.
                // Then the distance for an empty input is 1
                // (before applying the scale factor).
                if (termsLen == 0 && cenTermsL == 0)
                    return 0;
                if (termsLen == 0 || cenTermsL == 0)
                    return Math.Pow(scale, 2.0);

                int inputCount = 0;
                foreach (string term in centroidTerms) {
                    if (terms.Contains<string>(term))
                    {
                        inputCount += 1;
                    }
                }

                double cosineSimilarity = inputCount / Math.Sqrt(termsLen * cenTermsL);
                double similarityDistance = scale * (1 - cosineSimilarity);
                return Math.Pow(similarityDistance, 2.0);
            }
        }
    }
}