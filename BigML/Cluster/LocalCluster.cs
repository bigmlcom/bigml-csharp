using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;


namespace BigML
{
    public partial class Cluster
    {
        public class LocalCluster
        {
            readonly JObject _jsonObject;
            readonly Dictionary<string, DataSet.Field> _fields;

            string resourceId;
            List<LocalCentroid> centroids;
            JObject[] theClusters;
            JObject clusterGlobal;
            int totalSs = 0;
            int withIn = 0;
            int betweenSs = 0;
            float ratioSs = 0;
            float? criticalValue;
            string defaultNumericValue;
            int k = 0;
            List<string> summaryFields = null;
            Dictionary<string, double> scales;
            Dictionary<string, Dictionary<string, string[]>> termForms = new Dictionary<string, Dictionary<string, string[]>>();
            Dictionary<string, string[]> tagClouds;
            Dictionary<string, Dictionary<string, dynamic>> termAnalysis = new Dictionary<string, Dictionary<string, dynamic>>();
            Dictionary<string, Dictionary<string, dynamic>> itemAnalysis = new Dictionary<string, Dictionary<string, dynamic>>();
            Dictionary<string, string[]> items;

            Dictionary<string, string> nameToIdDict = new Dictionary<string, string>();
            Dictionary<string, bool> fieldAllowEmpty = new Dictionary<string, bool>();

            static string[] OptionalFields = { "categorical", "text",
                              "items", "datetime" };
            static string[] CsvStatistics =  { "minimum", "mean", "median",
                              "maximum", "standard_deviation",
                              "sum", "sum_squares", "variance" };

            /*INTERCENTROID_MEASURES = [('Minimum', min),
                          ('Mean', lambda(x): sum(x) / float(len(x))),
                          ('Maximum', max)]*/

            string GlobalClusterLabel = "Global";
            static string[] NumericDefaults = { "mean", "median", "minimum",
                               "maximum", "zero" };

            bool isGmeans() {
                return this.criticalValue != null;
            }


            static string[] getUniqueTerms(List<string> terms,
                                Dictionary<string, string[]> termForms,
                                string[] tagCloud)
            {
                Dictionary<string, string> extendForms = new Dictionary<string, string>();

                foreach (KeyValuePair<string, string[]> entry in termForms)
                {
                    foreach (string form in entry.Value)
                    {
                        extendForms[form] = entry.Key;
                    }
                    extendForms[entry.Key] = entry.Key;
                }

                HashSet<string> termsSet = new HashSet<string>();
                foreach (string term in terms)
                {
                    if (tagCloud.Contains(term))
                    {
                        termsSet.Add(term);
                    }
                    else if (extendForms.ContainsKey(term))
                    {
                        termsSet.Add(extendForms[term]);
                    }
                }
                return termsSet.ToArray<string>();
            }



            List<string> parseTerms(string text, bool case_sensitive = true) {
                List<string> result = new List<string>();

                if (text == null) {
                    return result;
                }

                string expression = "(\b|_)([^\b_\\s]+?)(\b|_)";

                foreach (string match in Regex.Matches(text, expression)) {
                    if (case_sensitive) {
                        result.Add(match);
                    }
                    else
                    {
                        result.Add(match.ToLower());
                    }
                }
                return result;
            }


            List<string> parseItems(string text, string regexp)
            {
                List<string> result = new List<string>();

                if (text == null)
                {
                    return result;
                }

                foreach (string match in Regex.Matches(text, regexp))
                {
                    result.Add(match);
                }

                return result;
            }


            public LocalCluster(JObject jsonObject)
            {
                _jsonObject = jsonObject;

                resourceId = (string)jsonObject["resource"];
                defaultNumericValue = (string)jsonObject["default_numeric_value"];

                summaryFields = new List<string>();
                foreach (JObject oneSummaryField in jsonObject["summary_fields"])
                {
                    summaryFields.Add((string)oneSummaryField);
                }

                centroids = new List<LocalCentroid>();
                foreach (JObject oneCentroid in jsonObject["clusters"]["clusters"])
                {
                    LocalCentroid c = new LocalCentroid(oneCentroid);
                    centroids.Add(c);
                }
                clusterGlobal = (JObject)jsonObject["clusters"]["global"];
                Dictionary<string, dynamic> fields = jsonObject["clusters"]["fields"].ToObject<Dictionary<string, dynamic>>();

                _fields = new Dictionary<string, DataSet.Field>();
                foreach (KeyValuePair<string, dynamic> fieldData in fields) {
                    string fieldId = fieldData.Key;
                    DataSet.Field field = new DataSet.Field(fieldData.Value);
                    _fields[fieldId] = field;
                    nameToIdDict.Add(_fields[fieldId].Name, fieldId);
                    fieldAllowEmpty[fieldId] = _fields[fieldId].OpType.Equals(OpType.Text) || _fields[fieldId].OpType.Equals(OpType.Items);
                }

                scales = jsonObject["scales"].ToObject<Dictionary<string, double>>();
            }

            Dictionary<string, dynamic>
            fillNumericDefaults(Dictionary<string, dynamic> inputData, string average = "mean")
            {

                foreach (KeyValuePair<string, DataSet.Field> idAndField in _fields)
                {
                    float default_value;
                    DataSet.Field field = idAndField.Value;
                    string fieldId = idAndField.Key;

                    if (summaryFields.IndexOf(fieldId) > -1 &&
                            OptionalFields.Contains<string>(field.Optype.ToString()) &&
                            !inputData.ContainsKey(fieldId)) {

                        if (!NumericDefaults.Contains<string>(average)) {
                            throw (new System.Exception("The available defaults are: %s" +
                                    string.Join(",", NumericDefaults)));
                        }

                        if (average == "zero")
                        {
                            default_value = 0;
                        }
                        else
                        {
                            // get property by reflection
                            default_value = (float)field.FieldSummary.GetType().GetProperty(average).GetValue(field.FieldSummary, null);
                        }
                        inputData[fieldId] = default_value;
                    }
                }

                return inputData;
            }


            Dictionary<string, string[]> getUniqueTerms(Dictionary<string, dynamic> inputData)
            {
                Dictionary<string, string[]> uniqueTerms = new Dictionary<string, string[]>();
                dynamic inputDataField;

                foreach (string fieldId in this.termForms.Keys)
                {
                    if (inputData.ContainsKey(fieldId))
                    {
                        inputDataField = inputData[fieldId];

                        if (inputDataField.GetType() == "string")
                        {
                            bool caseSensitive = this.termAnalysis[fieldId]["case_sensitive"];
                            string tokenMode = this.termAnalysis[fieldId]["token_mode"];
                            List<string> terms;

                            if (tokenMode != "full_terms")
                            {
                                terms = parseTerms(inputDataField,
                                                    caseSensitive);
                            }
                            else
                            {
                                terms = new List<string>();
                            }

                            if (tokenMode != "tokens")
                            {
                                if (caseSensitive)
                                {
                                    terms.Add(inputDataField);
                                }
                                else
                                {
                                    terms.Add(inputDataField.toLower());
                                }
                            }

                            uniqueTerms[fieldId] = LocalCluster.getUniqueTerms(
                                terms, this.termForms[fieldId],
                                this.tagClouds[fieldId]);

                        }
                        else
                        {
                            uniqueTerms[fieldId] = inputDataField;
                        }
                        inputData.Remove(fieldId);
                    }
                }

                // the same for items fields
                foreach (string fieldId in this.itemAnalysis.Keys) {
                    if (inputData.ContainsKey(fieldId))
                    {
                        inputDataField = inputData[fieldId];

                        if (inputDataField.GetType() == "string")
                        {
                            // parsing the items in inputData
                            string separator = this.itemAnalysis[fieldId]["separator"];
                            string regexp = this.itemAnalysis[fieldId]["separator_regexp"];
                            List<string> terms;

                            if (regexp == null)
                            {
                                regexp = Regex.Escape(separator);
                            }
                            terms = parseItems(inputDataField, regexp);
                            uniqueTerms[fieldId] = LocalCluster.getUniqueTerms(
                                    terms, new Dictionary<string, string[]>(),
                                    this.items[fieldId]);
                        }
                        else
                        {
                            uniqueTerms[fieldId] = inputDataField;
                            inputData.Remove(fieldId);
                        }
                    }
                }
           
                return uniqueTerms;
            }


            string fieldId;
            Dictionary<string, dynamic> inputDataByFieldId;

            /// <summary>
            /// Clean input data introduced for a prediction removing fields 
            /// without valid value and populates the result's Dictionary
            /// by fieldID
            /// </summary>
            /// <param name="inputData">Original input data</param>
            /// <returns>A map by field ID and clean values</returns>
            public Dictionary<string, dynamic> filterInputData(Dictionary<string, dynamic> inputData, bool byName = true)
            {
                inputDataByFieldId = new Dictionary<string, dynamic>();

                foreach (string key in inputData.Keys)
                {
                    if (nameToIdDict.ContainsKey(key) && byName)
                    {
                        fieldId = nameToIdDict[key];
                        inputDataByFieldId[fieldId] = inputData[key];
                    }
                    else
                    {
                        fieldId = key;
                        inputDataByFieldId[key] = inputData[key];
                    }

                    // remove empty numbers or categoricals if it's a model field
                    if (!fieldAllowEmpty.ContainsKey(fieldId) ||
                        (!fieldAllowEmpty[fieldId] &&
                        inputDataByFieldId[fieldId].ToString() == ""))
                    {
                        inputDataByFieldId.Remove(fieldId);
                    }
                }
                return inputDataByFieldId;
            }


            /// <summary>
            /// Generates a prediction based on this Centroid
            /// </summary>
            /// <param name="inputData">Map with the ID or name of each field and its value</param>
            /// <param name="byName">Flag for the inputData key: 
            ///                         * true if fieldName is the key,
            ///                         * false if the key is the field ID</param>
            /// <returns>The nearest centroid data for the inputData</returns>

            public LocalCentroid centroid(Dictionary<string, dynamic> inputData, bool byName=true)
            {
                LocalCentroid nearest;
                dynamic cleanInputData = filterInputData(inputData, byName);

                try
                {
                    fillNumericDefaults(inputData);
                }
                catch (System.Exception e)
                {
                    throw new System.Exception(
                                "Failed to predict a centroid. Input" +
                                " data must contain values for all " +
                                "numeric fields to find a centroid. \n" + e);
                }

                Dictionary<string, string[]> uniqueTerms = getUniqueTerms(cleanInputData);

                nearest = new LocalCentroid();
                nearest.distance = int.MaxValue;

                foreach (LocalCentroid centroid in this.centroids) {
                    double? distance2 = centroid.distance2(cleanInputData, uniqueTerms,
                                                   this.scales,
                                                   nearest.distance);

                    if (distance2 != null) {
                        nearest.centroidId = centroid.centroidId;
                        nearest.centroidName = centroid.centroidName;
                        nearest.distance = distance2;
                    }
                }
                nearest.distance = System.Math.Sqrt((double) nearest.distance);

                return nearest;
            }
        }
    }
}

