using System.Collections.Generic;
using System.Json;


namespace BigML
{
    public partial class Ensemble
    {
        public class LocalEnsemble
        {
            readonly JsonValue _jsonObject;
            readonly JsonArray _modelResourcesIds;
            public List<Model.LocalModel> _models = new List<Model.LocalModel>();
            Model.Node[] _modelsPredictions;
            MultiVote mv;
            int i;

            internal LocalEnsemble(JsonValue jsonObject, JsonArray modelIds)
            {
                _jsonObject = jsonObject;
                _modelResourcesIds = modelIds;
                _modelsPredictions = new Model.Node[_modelResourcesIds.Count];
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

                if (_models.Count > 1) { 
                    inputData = _models[0].prepareInputData(inputData);
                    byName = false;
                }

                for (i = 0; i < this._models.Count; i++)
                {
                    _modelsPredictions[i] = this._models[i].predict(inputData, byName, missing_strategy);
                    mv.append(_modelsPredictions[i].toDictionary(addDistribution));
                }
                return mv.combine((int)combiner, addDistribution: addDistribution);
            }
        }
    }
}

