using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace BigML
{
    public partial class Ensemble
    {
        public class LocalEnsemble
        {
            readonly JObject _jsonObject;
            readonly JArray _modelResourcesIds;
            public List<Model.LocalModel> _models = new List<Model.LocalModel>();
            Model.Node[] _modelsPredictions;
            Model.BoostedNode[] _modelsBoostedPredictions;
            MultiVote mv;
            int i;


            internal LocalEnsemble(JObject jsonObject, JArray modelIds)
            {
                _jsonObject = jsonObject;
                _modelResourcesIds = modelIds;
                _modelsPredictions = new Model.Node[_modelResourcesIds.Count];
                _modelsBoostedPredictions = new Model.BoostedNode[_modelResourcesIds.Count];
            }


            public bool areBoostedTrees
            {
                get { return this._jsonObject["boosting"].Type != JTokenType.Null; }
            }


            public int addLocalModel(Model.LocalModel localModel)
            {
                this._models.Add(localModel);
                return this._models.Count;
            }


            public Dictionary<object, object> predict(Dictionary<string, dynamic> inputData,
                                                      bool byName = true,
                                                      Combiner combiner = Combiner.Plurality,
                                                      int missing_strategy = 0,
                                                      bool addDistribution=true)
            {
                mv = new MultiVote();
                bool addConfidence = areBoostedTrees;

                if (_models.Count > 1) { 
                    inputData = _models[0].prepareInputData(inputData);
                    byName = false;
                }

                mv.areBoostedTrees = areBoostedTrees;
                Dictionary<object, object> vote;
                for (i = 0; i < this._models.Count; i++)
                {
                    if (areBoostedTrees)
                    {
                        _modelsBoostedPredictions[i] = this._models[i].predictBoosted(inputData, byName, missing_strategy);

                        vote = _modelsBoostedPredictions[i].toDictionary();
                        vote["weight"] = this._models[i].Boosting["weight"];
                        if (this._models[i].Boosting.ContainsKey("objective_class"))
                        {
                            vote["class"] = this._models[i].Boosting["objective_class"];
                        }
                        mv.append(vote);
                    } else
                    {
                        _modelsPredictions[i] = this._models[i].predict(inputData, byName, missing_strategy);
                        mv.append(_modelsPredictions[i].toDictionary(addDistribution));
                    }
                }
                return mv.combine((int)combiner, addDistribution: addDistribution, addConfidence:addConfidence);
            }
        }
    }
}

