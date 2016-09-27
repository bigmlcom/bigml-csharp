/*
 * Copyright 2012-2016 BigML
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License. You may obtain
 * a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Json;

namespace BigML
{

    /// A multiple vote prediction
    /// 
    /// Uses a number of predictions to generate a combined prediction.
    /// 
    public class MultiVote
    {

        const string PLURALITY = "plurality";
        const string CONFIDENCE = "confidence weighted";
        const string PROBABILITY = "probability weighted";
        const string THRESHOLD = "threshold";

        const int PLURALITY_CODE = 0;
        const int CONFIDENCE_CODE = 1;
        const int PROBABILITY_CODE = 2;
        const int THRESHOLD_CODE = 3;

        readonly string[] WEIGHT_LABELS = { "plurality", "confidence", "probability", "threshold" };

        readonly string[] PREDICTION_HEADERS = {"prediction", "confidence", "order", "distribution", "count" };

        readonly Dictionary<String, String> COMBINATION_WEIGHTS = new Dictionary<String, String>(){
            { PLURALITY, null } ,
            { CONFIDENCE, "confidence" },
            { PROBABILITY, "probability" },
            { THRESHOLD, null }
        };

        readonly Dictionary<int, String> COMBINER_MAP = new Dictionary <int, String>() {
            { PLURALITY_CODE, PLURALITY },
            { CONFIDENCE_CODE, CONFIDENCE },
            { PROBABILITY_CODE, PROBABILITY },
            { THRESHOLD_CODE, THRESHOLD }
        };

        readonly Dictionary<string, String[]> WEIGHT_KEYS = new Dictionary<string, String[]>() {
                { PLURALITY, null },
                { CONFIDENCE,  new String[]{"confidence" } },
                { PROBABILITY, new String[]{"distribution", "count" } },
                { THRESHOLD, null}
            };



        const int DEFAULT_METHOD = 0;
        const int BINS_LIMIT = 32;


        public Dictionary<object, object>[] predictions;

        /// <summary>
		/// MultiVote: combiner class for ensembles voting predictions.
		/// </summary>
		public MultiVote() : this(null)
        {
        }

        /// <summary>
        /// MultiVote: combiner class for ensembles voting predictions.
        /// </summary>
        /// <param name="predictionsArr"> {array|object} predictions Array of model's predictions </param>
        public MultiVote(Dictionary<object, object>[] predictionsArr)
        {
            int i, len;
            if (predictionsArr == null)
            {
                predictionsArr = new Dictionary<object, object>[0];
            }
            predictions = predictionsArr;

            bool allOrdered = true;
            for (i = 0, len = predictions.Length; i < len; i++)
            {
                if (!predictions[i].ContainsKey("order"))
                {
                    allOrdered = false;
                    break;
                }
            }
            if (!allOrdered)
            {
                for (i = 0, len = predictions.Length; i < len; i++)
                {
                    predictions[i]["order"] = i;
                }
            }
        }

        public virtual Dictionary<object, object>[] Predictions
        {
            get
            {
                return predictions;
            }
        }




        /// <summary>
        /// Check if this is a regression model
        /// </summary>
        /// <returns> {boolean} True if all the predictions are numbers. </returns>
        private bool is_regression()
        {
            int index, len;
            Dictionary<object, object> prediction;
            for (index = 0, len = this.predictions.Length; index < len; index++)
            {
                prediction = this.predictions[index];
                if (!(Utils.isNumericType(prediction["prediction"])))
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// Return the next order to be assigned to a prediction
        /// 
        /// Predictions in MultiVote are ordered in arrival sequence when
        /// added using the constructor or the append and extend methods.
        /// This order is used to break even cases in combination
        /// methods for classifications.
        /// </summary>
        /// <returns> the next order to be assigned to a prediction </returns>
        private int nextOrder()
        {
            if (predictions != null && predictions.Length > 0)
            {
                return ((int) predictions[predictions.Length - 1]["order"]) + 1;
            }

            return 0;
        }


        /// <summary>
        /// Given a multi vote instance (alist of predictions), extends the list
        /// with another list of predictions and adds the order information.
        /// 
        /// For instance, predictions_info could be:
        /// 
        ///  [{'prediction': 'Iris-virginica', 'confidence': 0.3},
        ///      {'prediction': 'Iris-versicolor', 'confidence': 0.8}]
        /// 
        ///  where the expected prediction keys are: prediction (compulsory),
        ///  confidence, distribution and count.
        /// </summary>
        /// <param name="votes"> </param>
        public virtual void extend(MultiVote votes)
        {
            if (votes.predictions != null && votes.predictions.Length > 0)
            {
                int order = nextOrder();

                List<Dictionary<object, object>> predictionsList = new List<Dictionary<object, object>>(predictions);

                foreach (Dictionary<object, object> prediction in votes.predictions)
                {
                    prediction["order"] = (order + 1);
                    predictionsList.Add(prediction);
                }

                predictions = (Dictionary<object, object>[])predictionsList.ToArray();
            }
        }


        /// <summary>
        /// Checks the presence of each of the keys in each of the predictions
        /// </summary>
        /// <param name="predictions"> {array} predictions Array of prediction objects </param>
        /// <param name="keys"> {array} keys Array of key strings </param>
        private static bool checkKeys(Dictionary<object, object>[] predictions, string[] keys)
        {
            Dictionary<object, object> prediction;
            string key;
            int index, kindex, len;
            for (index = 0, len = predictions.Length; index < len; index++)
            {
                prediction = predictions[index];
                for (kindex = 0; kindex < keys.Length; kindex++)
                {
                    key = keys[kindex];
                    if (!prediction.ContainsKey(key))
                    {
                        throw new Exception("Not enough data to use the selected prediction method.");
                    }
                }
            }
            return true;
        }


        /// Wilson score interval computation of the distribution for the prediction
        /// expected arguments:
        ///         prediction: the value of the prediction for which confidence is computed
        ///         distribution: a distribution-like structure of predictions and
        ///                        the associated weights. (e.g. [['Iris-setosa', 10], ['Iris-versicolor', 5]])
        ///         ws_z: percentile of the standard normal distribution
        ///         ws_n: total number of instances in the distribution.If absent, the number is computed as the sum of weights in the
        ///               provided distribution

        private double wsConfidence(string prediction, Dictionary<string, double?> distribution, double ws_z = 1.96, double? ws_n = null)
        {
            double? ws_p;
            double ws_norm, ws_z2, ws_factor, ws_sqrt;
            distribution.TryGetValue(prediction, out ws_p);
            
            if (ws_p < 0)
            {
                throw new Exception("The distribution weight must be a positive value");
            }

            ws_norm = 0.0d;
            foreach (string key in distribution.Keys)
            {
                ws_norm += (double) distribution[key];
            }

            if (ws_p < 0)
            {
                throw new Exception("Invalid distribution " + distribution );
            }

            if (ws_norm != 1.0)
            {
                ws_p = ws_p / ws_norm;
            }
            if (ws_n == null)
            {
                ws_n = (int) ws_norm;
            }
            else
            {
                ws_n = (double) ws_n;
            }
            if (ws_n < 1)
            {
                throw new Exception("The total of instances in the distribution must be a positive integer");
            }
            ws_z2 = ws_z * ws_z;
            ws_factor = ws_z2 / (double) ws_n;
            ws_sqrt = Math.Sqrt(((double)ws_p * (1 - (double)ws_p) + ws_factor / 4) / (double)ws_n);
            return ((double)ws_p + ws_factor / 2 - ws_z * ws_sqrt) / (1 + ws_factor);
        }


        /// <summary>
		/// Average for regression models' predictions
		/// 
		/// </summary>
		private Dictionary<object, object> avg(bool? withConfidence = false, bool? addConfidence = false,
                                                bool? addDistribution = false, bool? addCount = false,
                                                bool? addMedian = false)
        {

            int i, len, total = this.predictions.Length;
            double result = 0.0d, confidence = 0.0d, medianResult = 0.0d;
            Dictionary<object, object> average = new Dictionary<object, object>();
            long instances = 0;

            for (i = 0, len = this.predictions.Length; i < len; i++)
            {
                result += Convert.ToDouble(this.predictions[i]["prediction"]);

                if (addMedian.Value)
                {
                    medianResult += Convert.ToDouble(this.predictions[i]["median"]);
                }

                confidence += Convert.ToDouble(this.predictions[i]["confidence"]);

                instances += Convert.ToInt64(this.predictions[i]["count"]);
            }

            if (total > 0)
            {
                average["prediction"] = result / total;
                average["confidence"] = confidence / total;
            }
            else
            {
                average["prediction"] = Double.NaN;
                average["confidence"] = 0.0d;
            }
            // Equivalent to:
            //  average.putAll(getGroupedDistribution(this));
            Dictionary<string, object> groupedDistribution = getGroupedDistribution(this);
            foreach (var oneKey in groupedDistribution.Keys)
            {
                average[oneKey] = groupedDistribution[oneKey];
            }
            average["count"] = instances;

            if (addMedian.Value)
            {
                if (total > 0)
                {
                    average["median"] = medianResult / total;
                }
                else
                {
                    average["median"] = Double.NaN;
                }
            }

            return average;
        }


        /// <summary>
        /// Returns a distribution formed by grouping the distributions of each predicted node.
        /// </summary>
        protected internal Dictionary<string, object> getGroupedDistribution(MultiVote multiVoteInstance)
        {
            Dictionary<object, double> joinedDist = new Dictionary<object, double>();
            string distributionUnit = "counts";

            foreach (Dictionary<object, object> prediction in multiVoteInstance.Predictions)
            {
                
                // Equivalent to:
                //  JSONArray predictionDist = (JSONArray) prediction.get("distribution");
                Dictionary<object, double> predictionDist = null;
                object distribution = prediction["distribution"];

                if (distribution is IDictionary)
                {
                    predictionDist = (Dictionary<object, double>) distribution;
                }
                else if ((distribution == null) || (!(distribution is JsonArray)))
                {
                    predictionDist = new Dictionary<object, double>();
                }
                else
                {
                    predictionDist = Utils.convertDistributionArrayToMap((JsonArray) distribution);
                }

                // Equivalent to:
                //  joinedDist = Utils.mergeDistributions(joinedDist, Utils.convertDistributionArrayToMap(predictionDist));
                joinedDist = Utils.mergeDistributions(joinedDist, predictionDist);

                if ("counts".Equals(distributionUnit) && joinedDist.Count > BINS_LIMIT)
                {
                    distributionUnit = "bins";
                }

                joinedDist = Utils.mergeBins(joinedDist, BINS_LIMIT);
            }

            Dictionary<string, object> distributionInfo = new Dictionary<string, object>();
            distributionInfo["distribution"] = Utils.convertDistributionMapToSortedArray(joinedDist);
            distributionInfo["distributionUnit"] = distributionUnit;

            return distributionInfo;
        }



        private bool isValueIn(dynamic value, object[][] distribution)
        {
            for (int index = 0; index < distribution.Length; index++)
            {
                if (value == distribution[index][0])
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Returns the prediction combining votes using error to compute weight
        /// </summary>
        /// <returns> {{'prediction': {string|number}, 'confidence': {number}}} The
        ///         combined error is an average of the errors in the MultiVote
        ///         predictions. </returns>
        public virtual Dictionary<object, object> errorWeighted(bool? withConfidence = false, bool? addConfidence = false, bool? addDistribution = false, bool? addCount = false, bool? addMedian = false)
        {

            checkKeys(this.predictions, new string[] { "confidence" });
            int index, len;
            Dictionary<object, object> newPrediction = new Dictionary<object, object>();
            double? combinedError = 0.0d, topRange = 10.0d, result = 0.0d, medianResult = 0.0d, normalization_factor = this.normalizeError(topRange);
            long? instances = 0L;

            if (normalization_factor == 0.0d)
            {
                newPrediction["prediction"] = Double.NaN;
                newPrediction["confidence"] = 0.0d;
                return newPrediction;
            }

            for (index = 0, len = this.predictions.Length; index < len; index++)
            {
                Dictionary<object, object> prediction = this.predictions[index];

                result += ((double)prediction["prediction"]) * ((double)prediction["errorWeight"]);

                if (addMedian.Value)
                {
                    medianResult += ((double)prediction["median"]) * ((double)prediction["errorWeight"]);
                }

                instances += ((long) prediction["count"]);

                combinedError += ((double)prediction["confidence"]) * ((double)prediction["errorWeight"]);
            }

            newPrediction["prediction"] = result / normalization_factor;
            newPrediction["confidence"] = combinedError / normalization_factor;
            newPrediction["count"] = instances;
            if (addMedian.Value)
            {
                newPrediction["median"] = medianResult / normalization_factor;
            }

            // Equivalent to:
            //  newPrediction.putAll(getGroupedDistribution(this));
            Dictionary<string, object> groupedDistribution = getGroupedDistribution(this);
            foreach (var oneKey in groupedDistribution.Keys)
            {
                newPrediction[oneKey] = groupedDistribution[oneKey];
            }

            return newPrediction;
        }


        /// <summary>
        /// Normalizes error to a [0, top_range] range and builds probabilities
        /// </summary>
        /// <param name="topRange"> {number} The top range of error to which the original error is
        ///        normalized. </param>
        /// <returns> {number} The normalization factor as the sum of the normalized
        ///         error weights. </returns>
        public virtual double? normalizeError(double? topRange)
        {
            int index, len;
            double? error, errorRange, delta;
            double maxError = -1.0d, minError = double.MaxValue, normalizeFactor = 0.0d;
            Dictionary<object, object> prediction;
            for (index = 0, len = this.predictions.Length; index < len; index++)
            {
                prediction = this.predictions[index];
                if (!prediction.ContainsKey("confidence"))
                {
                    throw new Exception("Not enough data to use the selected prediction method.");
                }
                error = ((double) prediction["confidence"]);
                maxError = Math.Max((double) error, (double) maxError);
                minError = Math.Min((double) error, (double) minError);
            }
            errorRange = maxError - minError;
            normalizeFactor = 0.0d;
            if (errorRange > 0.0d)
            {
                /*
				 * Shifts and scales predictions errors to [0, top_range]. Then
				 * builds e^-[scaled error] and returns the normalization factor to
				 * fit them between [0, 1]
				 */
                for (index = 0, len = this.predictions.Length; index < len; index++)
                {
                    prediction = this.predictions[index];
                    delta = (minError - ((double) prediction["confidence"]));
                    this.predictions[index]["errorWeight"] = Math.Exp((double) delta / (double) errorRange * (double) topRange);
                    normalizeFactor += (double) this.predictions[index]["errorWeight"];
                }
            }
            else
            {
                for (index = 0, len = this.predictions.Length; index < len; index++)
                {
                    prediction = this.predictions[index];
                    this.predictions[index]["errorWeight"] = 1.0d;
                }
                normalizeFactor = this.predictions.Length;
            }
            return normalizeFactor;
        }


        /// <summary>
        /// Creates a new predictions array based on the training data probability
        /// </summary>
        public virtual Dictionary<object, object>[] probabilityWeight()
        {
            int index, len, total, order;

            IDictionary<object, object> prediction = new Dictionary<object, object>();
            IList<IDictionary<object, object>> predictionsList = new List<IDictionary<object, object>>();

            for (index = 0, len = this.predictions.Length; index < len; index++)
            {
                prediction = this.predictions[index];
                if (!prediction.ContainsKey("distribution") || !prediction.ContainsKey("count"))
                {
                    throw new Exception("Probability weighting is not available because distribution information is missing.");
                }

                total = (int) prediction["count"];

                if (total < 1)
                {
                    throw new Exception("Probability weighting is not available because distribution seems to have " + total + " as number of instances in a node");
                }

                order = (int) prediction["order"];

                Hashtable map = (Hashtable)prediction["distribution"];
                foreach (object key in map.Keys)
                {
                    int? count = (int?)map[key];
                    Dictionary<object, object> predictionHash = new Dictionary<object, object>();
                    predictionHash["prediction"] = key;
                    predictionHash["probability"] = count.Value / total;
                    predictionHash["count"] = count.Value;
                    predictionHash["order"] = order;

                    predictionsList.Add(predictionHash);
                }
            }
            Dictionary<object, object>[] predictions = new Dictionary <object, object>[predictionsList.Count];
            for (index = 0, len = predictions.Length; index < len; index++)
            {
                predictions[index] = (Dictionary<object, object>)predictionsList[index];
            }
            return predictions;
        }



        /// <summary>
        /// Returns the prediction combining votes by using the given weight
        /// </summary>
        /// <param name="weightLabel"> {string} weightLabel Type of combination method: 'plurality':
        ///        plurality (1 vote per prediction) 'confidence': confidence
        ///        weighted (confidence as a vote value) 'probability': probability
        ///        weighted (probability as a vote value)
        /// 
        ///        Will also return the combined confidence, as a weighted average of
        ///        the confidences of the votes. </param>
        public virtual Dictionary<object, object> combineCategorical(string weightLabel, bool withConfidence = false)
        {

            int index, len;
            double weight = 1.0;
            object category;
            Dictionary<object, object> prediction = new Dictionary<object, object>();
            Dictionary<object, object> mode = new Dictionary<object, object>();
            ArrayList tuples = new ArrayList();

            for (index = 0, len = this.predictions.Length; index < len; index++)
            {
                prediction = this.predictions[index];

                if (!string.ReferenceEquals(weightLabel, null))
                {
                    if (Array.IndexOf(WEIGHT_LABELS, weightLabel) == -1)
                    {
                        throw new Exception("Wrong weightLabel value.");
                    }
                    if (!prediction.ContainsKey(weightLabel))
                    {
                        throw new Exception("Not enough data to use the selected prediction" + " method. Try creating your model anew.");
                    }
                    else
                    {
                        weight = (double) prediction[weightLabel];
                    }
                }

                category = prediction["prediction"];

                Dictionary<string, object> categoryDict = new Dictionary<string, object>();
                if (mode.ContainsKey(category))
                {
                    categoryDict["count"] = (double?) ((Dictionary<string, object>) mode[category])["count"] + weight;
                    categoryDict["order"] = ((Dictionary<string, object>)mode[category])["order"];
                }
                else
                {
                    categoryDict["count"] = weight;
                    categoryDict["order"] = prediction["order"];
                }

                mode[category] = categoryDict;

            }

            foreach (string categoryStr in mode.Keys)
            {
                dynamic[] oneTuple;
                if (mode[categoryStr] != null)
                {
                    oneTuple = new dynamic[2];
                    oneTuple[0] = categoryStr;
                    oneTuple[1] = (Dictionary<string, object>) mode[categoryStr];
                    tuples.Add(oneTuple);
                }
            }


            object weight1, weight2, order1, order2;
            for (int indx = 0; indx < tuples.Count - 1; indx++)
            {
                Dictionary<string, object> a = (Dictionary <string, object>) ((object[]) tuples[indx])[1];
                Dictionary<string, object> b = (Dictionary <string, object>) ((object[]) tuples[indx + 1])[1];
                
                a.TryGetValue("count", out weight1);
                b.TryGetValue("count", out weight2);
                if ((double) weight1 > (double) weight2)
                {
                    ;   //TODO
                } else if ((double) weight1 < (double) weight2) 
                {
                    ;   //TODO
                } else
                {
                    //counts are equals show order
                    a.TryGetValue("order", out order1);
                    b.TryGetValue("order", out order2);

                    if (Convert.ToDouble(order1) > Convert.ToDouble(order2))
                    {
                        ;   //TODO
                    } 
                    else if (Convert.ToDouble(order1) < Convert.ToDouble(order2))
                    {
                        ;   //TODO
                    }
                }
                
            }

            object[] tuple = (object[])tuples[0];
            object predictionName = tuple[0];

            Dictionary<object, object> result = new Dictionary<object, object>();
            result["prediction"] = predictionName;

            if (withConfidence)
            {
                if (this.predictions[0]["confidence"] != null)
                {
                    return this.weightedConfidence(predictionName, weightLabel);
                }

                // If prediction had no confidence, compute it from distribution
                object[] distributionInfo = this.combineDistribution(weightLabel);
                int count = (int) distributionInfo[1];
                Dictionary<string, double?> distribution = (Dictionary<string, double?>)distributionInfo[0];

                double combinedConfidence = wsConfidence((string) predictionName, distribution, count, null);

                result["confidence"] = combinedConfidence;
            }

            return result;
        }


        /// <summary>
		/// Compute the combined weighted confidence from a list of predictions
		/// </summary>
		/// <param name="combinedPrediction"> {object} combinedPrediction Prediction object </param>
		/// <param name="weightLabel"> {string} weightLabel Label of the value in the prediction object
		///        that will be used to weight confidence </param>
		public virtual Dictionary<object, object> weightedConfidence(object combinedPrediction, object weightLabel)
        {
            int index, len;
            double? finalConfidence = 0.0;
            double weight = 1.0;
            double totalWeight = 0.0;
            Dictionary<object, object> prediction = null;
            ArrayList predictionsList = new ArrayList();

            for (index = 0, len = this.predictions.Length; index < len; index++)
            {
                if (this.predictions[index]["prediction"].Equals(combinedPrediction))
                {
                    predictionsList.Add(this.predictions[index]);
                }
            }
            // Convert to array
            Dictionary<object, object>[] predictions = new Dictionary<object, object>[predictionsList.Count];
            for (index = 0, len = predictions.Length; index < len; index++)
            {
                predictions[index] = (Dictionary<object, object>)predictionsList[index];
            }

            if (weightLabel != null)
            {
                for (index = 0, len = this.predictions.Length; index < len; index++)
                {
                    prediction = this.predictions[index];
                    if (prediction["confidence"] == null || prediction[weightLabel] == null)
                    {
                        throw new Exception("Not enough data to use the selected prediction method. Lacks " + weightLabel + " information");
                    }
                }
            }

            for (index = 0, len = predictions.Length; index < len; index++)
            {
                prediction = predictions[index];

                if (weightLabel != null)
                {
                    weight = ((double) prediction["confidence"]);
                }
                finalConfidence += weight * (float) prediction["confidence"];
                totalWeight += weight;
            }

            if (totalWeight > 0)
            {
                finalConfidence = finalConfidence / totalWeight;
            }
            else
            {
                finalConfidence = null;
            }

            Dictionary<object, object> result = new Dictionary<object, object>();
            result["prediction"] = combinedPrediction;
            result["confidence"] = finalConfidence;

            return result;
        }


        /// <summary>
        /// Builds a distribution based on the predictions of the MultiVote
        /// </summary>
        /// <param name="weightLabel"> {string} weightLabel Label of the value in the prediction object
        ///        whose sum will be used as count in the distribution </param>
        public virtual object[] combineDistribution(string weightLabel)
        {
            int index, len;
            int total = 0;
            Dictionary<object, object> prediction = new Dictionary<object, object>();
            Dictionary<string, double?> distribution = new Dictionary<string, double?>();
            object[] combinedDistribution = new object[2];

            if (string.ReferenceEquals(weightLabel, null) || weightLabel.Trim().Length == 0)
            {
                weightLabel = WEIGHT_LABELS[PROBABILITY_CODE];
            }


            for (index = 0, len = this.predictions.Length; index < len; index++)
            {
                prediction = this.predictions[index];

                if (!prediction.ContainsKey(weightLabel))
                {
                    throw new Exception("Not enough data to use the selected prediction method. Try creating your model anew.");
                }

                string predictionName = (string)prediction["prediction"];
                if (!distribution.ContainsKey(predictionName))
                {
                    distribution[predictionName] = 0.0;
                }

                distribution[predictionName] = distribution[predictionName] + (double?)prediction[weightLabel];
                total += (int) prediction["count"];
            }

            combinedDistribution[0] = distribution;
            combinedDistribution[1] = total;
            return combinedDistribution;
        }

        /// <summary>
        /// Reduces a number of predictions voting for classification and averaging
        /// predictions for regression using the PLURALITY method and without confidence
        /// </summary>
        /// <returns> {{"prediction": prediction}} </returns>
        public virtual Dictionary<object, object> combine()
        {
            return combine(PLURALITY_CODE, false, null, null, null, null, null);
        }


        /// <summary>
        /// Reduces a number of predictions voting for classification and averaging
        /// predictions for regression.
        /// </summary>
        /// <param name="method"> {0|1|2|3} method Code associated to the voting method (plurality,
        ///        confidence weighted or probability weighted or threshold). </param>
        /// <param name="withConfidence"> if withConfidence is true, the combined confidence
        ///                       (as a weighted of the prediction average of the confidences
        ///                       of votes for the combined prediction) will also be given. </param>
        /// <returns> {{"prediction": prediction, "confidence": combinedConfidence}} </returns>
        public virtual Dictionary<object, object> combine(int method = PLURALITY_CODE, bool withConfidence = false,
                                                            bool? addConfidence = false, bool? addDistribution = false, 
                                                            bool? addCount = false, bool? addMedian = false,
                                                            IDictionary options = null)
        {

            // there must be at least one prediction to be combined
            if (this.predictions.Length == 0)
            {
                throw new Exception("No predictions to be combined.");
            }

            string[] keys = WEIGHT_KEYS[COMBINER_MAP[method]];
            // and all predictions should have the weight-related keys
            if (keys !=  null && keys.Length > 0)
            {
                checkKeys(this.predictions, keys);
            }

            if (this.is_regression())
            {
                foreach (Dictionary<object, object> prediction in predictions)
                {
                    if (!prediction.ContainsKey("confidence"))
                    {
                        prediction["confidence"] = 0.0;
                    }
                }

                if (method == CONFIDENCE_CODE)
                {
                    return this.errorWeighted(withConfidence, addConfidence, addDistribution, addCount, addMedian);
                }
                return this.avg(withConfidence, addConfidence, addDistribution, addCount, addMedian);
            }

            // Categorical ensemble
            MultiVote multiVote = null;
            if (method == THRESHOLD_CODE)
            {
                int? threshold = (int?)options["threshold"];
                string category = (string)options["category"];

                multiVote = singleOutCategory(threshold, category);
            }
            else if (method == PROBABILITY_CODE)
            {
                multiVote = new MultiVote(this.probabilityWeight());
            }
            else
            {
                multiVote = this;
            }
            return multiVote.combineCategorical(COMBINATION_WEIGHTS[COMBINER_MAP[method]], withConfidence);
        }


        /// <summary>
        /// Adds a new prediction into a list of predictions
        /// 
        /// prediction_info should contain at least:
        ///      - prediction: whose value is the predicted category or value
        /// 
        /// for instance:
        ///      {'prediction': 'Iris-virginica'}
        /// 
        /// it may also contain the keys:
        ///      - confidence: whose value is the confidence/error of the prediction
        ///      - distribution: a list of [category/value, instances] pairs
        ///                      describing the distribution at the prediction node
        ///      - count: the total number of instances of the training set in the
        ///                  node
        /// </summary>
        /// <param name="predictionInfo"> the prediction to be appended </param>
        /// <returns> the this instance </returns>
        public virtual MultiVote append(Dictionary<object, object> predictionInfo)
        {

            if (predictionInfo == null || predictionInfo.Count == 0 || !predictionInfo.ContainsKey("prediction"))
            {
                throw new System.ArgumentException("Failed to add the prediction.\\n" +
                                        "The minimal key for the prediction is 'prediction'" +
                                        ":\n{'prediction': 'Iris-virginica'");
            }

            int order = nextOrder();
            predictionInfo["order"] = order;
            //Dictionary<object, object>[] temp = predictions.Clone();
            Dictionary<object, object>[] temp = new Dictionary<object, object>[predictions.Length];
            Array.Copy(predictions, 0, temp, 0, temp.Length);

            predictions = new Dictionary<object, object>[predictions.Length + 1];

            Array.Copy(temp, 0, predictions, 0, temp.Length);
            predictions[order] = predictionInfo;

            return this;
        }


        /// <summary>
        /// Singles out the votes for a chosen category and returns a prediction
        ///  for this category if the number of votes reaches at least the given
        ///  threshold.
        /// </summary>
        /// <param name="threshold"> the number of the minimum positive predictions needed for
        ///                    a final positive prediction. </param>
        /// <param name="category"> the positive category </param>
        /// <returns> MultiVote instance </returns>
        protected internal virtual MultiVote singleOutCategory(int? threshold, string category)
        {
            if (threshold == null || string.ReferenceEquals(category, null) || category.Length == 0)
            {
                throw new System.ArgumentException("No category and threshold information was found. Add threshold and category info." + 
                                        " E.g. {\"threshold\": 6, \"category\": \"Iris-virginica\"}.");
            }

            if (threshold > predictions.Length)
            {
                throw new System.ArgumentException("You cannot set a threshold value larger than " +
                    predictions.Length + ". The ensemble has not enough models to use this threshold value.");
            }

            if (threshold < 1)
            {
                throw new System.ArgumentException("The threshold must be a positive value");
            }

            ArrayList categoryPredictions = new ArrayList();
            ArrayList restOfPredictions = new ArrayList();

            foreach (Dictionary<object, object> prediction in predictions)
            {
                if (category.Equals(prediction["prediction"]))
                {
                    categoryPredictions.Add(prediction);
                }
                else
                {
                    restOfPredictions.Add(prediction);
                }
            }

            if (categoryPredictions.Count >= threshold)
            {
                return new MultiVote((Dictionary<object, object>[]) categoryPredictions.ToArray(typeof(Dictionary<object, object>)));
            }
            else
            {
                return new MultiVote((Dictionary<object, object>[]) restOfPredictions.ToArray(typeof(Dictionary<object, object>)));
            }
        }


        /// <summary>
        /// Adds a new prediction into a list of predictions
        /// 
        /// predictionHeaders should contain the labels for the predictionRow
        ///  values in the same order.
        /// 
        /// predictionHeaders should contain at least the following string
        ///      - 'prediction': whose associated value in predictionRow
        ///                      is the predicted category or value
        /// 
        /// for instance:
        ///      predictionRow = ['Iris-virginica']
        ///      predictionHeaders = ['prediction']
        /// 
        /// it may also contain the following headers and values:
        ///      - 'confidence': whose associated value in prediction_row
        ///                      is the confidence/error of the prediction
        ///      - 'distribution': a list of [category/value, instances] pairs
        ///                      describing the distribution at the prediction node
        ///      - 'count': the total number of instances of the training set in the
        ///                      node
        /// </summary>
        /// <param name="predictionRow"> the list of predicted values and extra info </param>
        /// <param name="predictionHeaders"> the name of each value in the predictionRow </param>
        /// <returns> the this instance </returns>
        public virtual MultiVote appendRow(IList<object> predictionRow, IList<string> predictionHeaders)
        {
            if (predictionHeaders == null)
            {
                predictionHeaders = PREDICTION_HEADERS;
            }

            if (predictionRow.Count != predictionHeaders.Count)
            {
                throw new System.ArgumentException("WARNING: failed to add the prediction.\\n" +
                            "The row must have label 'prediction' at least. And the number" + " of headers must much with the number of elements in the row.");
            }

            IList<object> mutablePredictionRow = new List<object>(predictionRow);
            IList<string> mutablePredictionHeaders = new List<string>(predictionHeaders);

            int index = -1;
            int order = nextOrder();
            try
            {
                index = mutablePredictionHeaders.IndexOf("order");
                mutablePredictionRow[index] = order;
            }
            catch (Exception)
            {
                mutablePredictionHeaders.Add("order");
                mutablePredictionRow.Add(order);
            }

            Dictionary<object, object> predictionInfo = new Dictionary<object, object>();
            for (int i = 0; i < mutablePredictionHeaders.Count; i++)
            {
                predictionInfo[mutablePredictionHeaders[i]] = mutablePredictionRow[i];
            }

            Dictionary<object, object>[] temp = new Dictionary<object, object>[predictions.Length];
            Array.Copy(predictions, 0, temp, 0, temp.Length);


            predictions = new Dictionary<object, object>[predictions.Length + 1];
            Array.Copy(temp, 0, predictions, 0, temp.Length);
            predictions[order] = predictionInfo;

            return this;
        }


        /// <summary>
        /// Given a list of predictions, extends the list with another list of
        ///  predictions and adds the order information. For instance,
        ///  predictionsInfo could be:
        /// 
        ///      [{'prediction': 'Iris-virginica', 'confidence': 0.3},
        ///       {'prediction': 'Iris-versicolor', 'confidence': 0.8}]
        /// 
        /// where the expected prediction keys are: prediction (compulsory),
        /// confidence, distribution and count.
        /// </summary>
        /// <param name="predictionsInfo"> the list of predictions we want to append </param>
        /// <returns> the this instance </returns>
        public virtual MultiVote extend(IList<Dictionary<object, object>> predictionsInfo)
        {

            if (predictionsInfo == null || predictionsInfo.Count == 0)
            {
                throw new System.ArgumentException("WARNING: failed to add the predictions.\\n" + 
                                                    "No predictions informed.");
            }

            int order = nextOrder();

            for (int i = 0; i < predictionsInfo.Count; i++)
            {
                Dictionary<object, object> prediction = predictionsInfo[i];
                prediction["order"] = order + i;
                append(prediction);
            }
            return this;
        }


        /// <summary>
		/// Given a list of predictions, extends the list with another list of
		///  predictions and adds the order information. For instance,
		///  predictionsInfo could be:
		/// 
		///      [{'prediction': 'Iris-virginica', 'confidence': 0.3},
		///       {'prediction': 'Iris-versicolor', 'confidence': 0.8}]
		/// 
		/// where the expected prediction keys are: prediction (compulsory),
		/// confidence, distribution and count.
		/// </summary>
		/// <param name="predictionsRows"> the list of predictions (in list format) we want to append </param>
		/// <returns> the this instance </returns>
		public virtual MultiVote extendRows(IList<IList<object>> predictionsRows, IList<string> predictionsHeader)
        {

            if (predictionsHeader == null)
            {
                predictionsHeader = PREDICTION_HEADERS;
            }

            int order = nextOrder();
            int index = predictionsHeader.IndexOf("order");
            if (index < 0)
            {
                index = predictionsHeader.Count;
                predictionsHeader.Add("order");
            }


            for (int iPrediction = 0; iPrediction < predictionsRows.Count; iPrediction++)
            {
                IList<object> predictionRow = predictionsRows[iPrediction];

                if (index == predictionRow.Count)
                {
                    predictionRow.Add(order + 1);
                }
                else
                {
                    predictionRow[index] = order;
                }

                appendRow(predictionRow, predictionsHeader);
            }

            return this;
        }

        /// <summary>
        /// Comparator
        /// </summary>
        internal class TupleComparator : IComparer<object[]>
        {
            private readonly MultiVote outerInstance;

            public TupleComparator(MultiVote outerInstance)
            {
                this.outerInstance = outerInstance;
            }

            public virtual int Compare(object[] o1, object[] o2)
            {
                Hashtable hash1 = (Hashtable)o1[1];
                Hashtable hash2 = (Hashtable)o2[1];
                double weight1 = (double) hash1["count"];
                double weight2 = (double) hash2["count"];
                int order1 = (int) hash1["order"];
                int order2 = (int) hash2["order"];
                return weight1 > weight2 ? -1 : (weight1 < weight2 ? 1 : order1 < order2 ? -1 : 1);
            }
        }

    }
}