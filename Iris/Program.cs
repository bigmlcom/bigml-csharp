using BigML;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Iris
{
    class Program
    {
        static void Main()
        {
            MainAsync().Wait();
            Console.ReadLine();
        }

        /// <summary>
        /// Simple sample that runs through all steps to explicitly create a prediction 
        /// from a csv file with the classic iris data.
        /// </summary>
        static async Task MainAsync()
        {
            // New BigML client with username and API key
            Console.Write("user: "); var User = Console.ReadLine();
            Console.Write("key: "); var ApiKey = Console.ReadLine();


            Ordered<Source.Filterable, Source.Orderable, Source> result
                = (from s in client.ListSources()
                   orderby s.Created descending
                   select s);

            var sources = await result;

            foreach(var src in sources)
            {
                Console.WriteLine(src.ToString());
            }
            /*
            // New source from in-memory stream, with separate header. That's the header
            var source = await client.CreateSource(iris, "Iris.csv", "sepal length, sepal width, petal length, petal width, species");
            // No push, so we need to busy wait for the source to be processed.
            while ((source = await client.Get(source)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
            Console.WriteLine(source.StatusMessage.ToString());

            // Default dataset from source
            var dataset = await client.CreateDataset(source);
            // No push, so we need to busy wait for the dataset to be processed.
            while ((dataset = await client.Get(dataset)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
            Console.WriteLine(dataset.StatusMessage.ToString());

            // Default model from dataset
            var model = await client.CreateModel(dataset);
            */
            /*
            Model model;
            string modelId = "model/575085112275c16672002f5d";
            // No push, so we need to busy wait for the source to be processed.
            while ((model = await client.Get<Model>(modelId)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);

            Dictionary<string, dynamic> inputData = new Dictionary<string, dynamic>();
            inputData.Add("000002", 3);
            inputData.Add("000003", 1.5);

            var localModel = model.ModelStructure;
            var nodoResultado = localModel.predict(inputData);

            Console.WriteLine("Predict:\n" + nodoResultado);
            */

            /*
            //MODELO CON campo de TEXTO
            Model model;
            string modelId = "model/5767b5963bbd213cac004a8d";
            while ((model = await client.Get<Model>(modelId)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
            Model.LocalModel localModel = model.ModelStructure;
            Dictionary<string, dynamic> inputData = new Dictionary<string, dynamic>();
            inputData.Add("000000", "");
            inputData.Add("000002", "salt in a recipe");
            inputData.Add("00000d", 0.1);
            Model.Node prediction = localModel.predict(inputData);
            */

            /*
            // modelo con items
            Model model;
            string modelId = "model/5767b5963bbd213cac004a8d";
            while ((model = await client.Get<Model>(modelId)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
            Model.LocalModel localModel = model.ModelStructure;
            Dictionary<string, dynamic> inputData = new Dictionary<string, dynamic>();
            inputData.Add("000000", "");
            inputData.Add("000002", "salt in a recipe");
            inputData.Add("00000d", 0.1);
            Model.Node prediction = localModel.predict(inputData);
            */


            
            //Ensemble ensemble;
            string modelId = "ensemble/5761a7fe3bbd210c6400a60a";
            string datasetId = "dataset/5761a4d73bbd210c6b005816";

            /*
            var parameters = new BatchPrediction.Arguments();
            // "model" parameter can be a model, an ensemble or a logisticregression
            parameters.Add("model", modelId);
            parameters.Add("dataset", datasetId);
            parameters.Add("output_dataset", true);
            BatchPrediction batchPrediction;
            batchPrediction = await client.CreateBatchPrediction(parameters);
            string batchPredictionId = batchPrediction.Resource;
            // wait for finish
            while ((batchPrediction = await client.Get<BatchPrediction>(batchPredictionId)).StatusMessage.NotSuccessOrFail()) await Task.Delay(10);
            Console.WriteLine(batchPrediction.OutputDatasetResource);
            */

            string ensembleId = "ensemble/565ecf4d28eb3e62f6000003"; //put your Id here
            var parameters = new Prediction.Arguments();
            parameters.Add("ensemble", ensembleId);
            parameters.InputData.Add("000000", 7.9);
            parameters.InputData.Add("000001", 3.8);
            parameters.InputData.Add("000002", 6.4);
            parameters.InputData.Add("000003", 2);
            Prediction ensemblePrediction = await client.CreatePrediction(parameters);
            Console.WriteLine(ensemblePrediction.GetPredictionOutcome<string>().ToString()); //output is string for categorical models


            // No push, so we need to busy wait for the source to be processed.
            /*while ((ensemble = await client.Get<Ensemble>(ensembleId)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
            var localEnsemble = ensemble.EnsembleStructure;
            Model modelInEnsemble;
            string modelId;
            for (int i = 0; i < ensemble.Models.Count; i++)
            {
                modelId = ensemble.Models[i];
                while ((modelInEnsemble = await client.Get<Model>(modelId)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
                localEnsemble.addLocalModel(modelInEnsemble.ModelStructure);
            }

            Dictionary<string, dynamic> inputData = new Dictionary<string, dynamic>();
            //inputData.Add("100000", 37);
            inputData.Add("Age", 37);
            inputData.Add("100001", "3rd Class");
            inputData.Add("100002", 7060);
            inputData.Add("100004", "servant");
            inputData.Add("100007", "1");

            var results = localEnsemble.predict(inputData);
            
            Console.WriteLine(results);
            */

            //Console.WriteLine("Predict:\n" + prediction);

            /*
            Console.WriteLine(model.StatusMessage.ToString());

            // The model description is what we are really after
            var description = model.ModelDescription;
            Console.WriteLine(description.ToString());

            Console.WriteLine("\n ---- Model description as a function (Output)     ------ ");
            // First convert it to a .NET expression tree
            var expression = description.Expression();
            Console.WriteLine(expression.ToString());

            Console.WriteLine("\n ---- Model description as a function (Confidence) ------ ");
            var expressionConf = description.ConfidenceValue();
            Console.WriteLine(expressionConf);

            Console.WriteLine("\n ---- Model description as a function (Object with properties) ------ ");
            var expressionCR = description.ComplexResult();
            Console.WriteLine(expressionCR);

            Console.WriteLine("\n Prediction block ------ ");
            // Then compile the expression tree into MSIL. Only with the input fields of the model
            var predictOutput = expression.Compile() as Func<double,double,double,double,string>;
            var predictConfid = expressionConf.Compile() as Func<double, double, double, double, float>;
            var predictBoth = expressionCR.Compile() as Func<double, double, double, double, Object>;

            // And try the first flower of the example set.
            var resultOutput = predictOutput(5.1, 3.5, 1.4, 0.2);
            var resultConfid = predictConfid(5.1, 3.5, 1.4, 0.2);
            var resultNode = predictBoth(5.1, 3.5, 1.4, 0.2);
            Console.WriteLine("Output = {0}, expected = {1}", resultOutput, "setosa");
            Console.WriteLine("Confidence = {0}", resultConfid);
            Console.WriteLine("Whole node values: {0}", ((Model.ResultNode)resultNode).ToString());
            */
        }

        private static IEnumerable<string> iris = new[]
        { "5.1,3.5,1.4,0.2,setosa"
        , "4.9,3.0,1.4,0.2,setosa"
        , "4.7,3.2,1.3,0.2,setosa"
        , "4.6,3.1,1.5,0.2,setosa"
        , "5.0,3.6,1.4,0.2,setosa"
        , "5.4,3.9,1.7,0.4,setosa"
        , "4.6,3.4,1.4,0.3,setosa"
        , "5.0,3.4,1.5,0.2,setosa"
        , "4.4,2.9,1.4,0.2,setosa"
        , "4.9,3.1,1.5,0.1,setosa"
        , "5.4,3.7,1.5,0.2,setosa"
        , "4.8,3.4,1.6,0.2,setosa"
        , "4.8,3.0,1.4,0.1,setosa"
        , "4.3,3.0,1.1,0.1,setosa"
        , "5.8,4.0,1.2,0.2,setosa"
        , "5.7,4.4,1.5,0.4,setosa"
        , "5.4,3.9,1.3,0.4,setosa"
        , "5.1,3.5,1.4,0.3,setosa"
        , "5.7,3.8,1.7,0.3,setosa"
        , "5.1,3.8,1.5,0.3,setosa"
        , "5.4,3.4,1.7,0.2,setosa"
        , "5.1,3.7,1.5,0.4,setosa"
        , "4.6,3.6,1.0,0.2,setosa"
        , "5.1,3.3,1.7,0.5,setosa"
        , "4.8,3.4,1.9,0.2,setosa"
        , "5.0,3.0,1.6,0.2,setosa"
        , "5.0,3.4,1.6,0.4,setosa"
        , "5.2,3.5,1.5,0.2,setosa"
        , "5.2,3.4,1.4,0.2,setosa"
        , "4.7,3.2,1.6,0.2,setosa"
        , "4.8,3.1,1.6,0.2,setosa"
        , "5.4,3.4,1.5,0.4,setosa"
        , "5.2,4.1,1.5,0.1,setosa"
        , "5.5,4.2,1.4,0.2,setosa"
        , "4.9,3.1,1.5,0.1,setosa"
        , "5.0,3.2,1.2,0.2,setosa"
        , "5.5,3.5,1.3,0.2,setosa"
        , "4.9,3.1,1.5,0.1,setosa"
        , "4.4,3.0,1.3,0.2,setosa"
        , "5.1,3.4,1.5,0.2,setosa"
        , "5.0,3.5,1.3,0.3,setosa"
        , "4.5,2.3,1.3,0.3,setosa"
        , "4.4,3.2,1.3,0.2,setosa"
        , "5.0,3.5,1.6,0.6,setosa"
        , "5.1,3.8,1.9,0.4,setosa"
        , "4.8,3.0,1.4,0.3,setosa"
        , "5.1,3.8,1.6,0.2,setosa"
        , "4.6,3.2,1.4,0.2,setosa"
        , "5.3,3.7,1.5,0.2,setosa"
        , "5.0,3.3,1.4,0.2,setosa"
        , "7.0,3.2,4.7,1.4,versicolor"
        , "6.4,3.2,4.5,1.5,versicolor"
        , "6.9,3.1,4.9,1.5,versicolor"
        , "5.5,2.3,4.0,1.3,versicolor"
        , "6.5,2.8,4.6,1.5,versicolor"
        , "5.7,2.8,4.5,1.3,versicolor"
        , "6.3,3.3,4.7,1.6,versicolor"
        , "4.9,2.4,3.3,1.0,versicolor"
        , "6.6,2.9,4.6,1.3,versicolor"
        , "5.2,2.7,3.9,1.4,versicolor"
        , "5.0,2.0,3.5,1.0,versicolor"
        , "5.9,3.0,4.2,1.5,versicolor"
        , "6.0,2.2,4.0,1.0,versicolor"
        , "6.1,2.9,4.7,1.4,versicolor"
        , "5.6,2.9,3.6,1.3,versicolor"
        , "6.7,3.1,4.4,1.4,versicolor"
        , "5.6,3.0,4.5,1.5,versicolor"
        , "5.8,2.7,4.1,1.0,versicolor"
        , "6.2,2.2,4.5,1.5,versicolor"
        , "5.6,2.5,3.9,1.1,versicolor"
        , "5.9,3.2,4.8,1.8,versicolor"
        , "6.1,2.8,4.0,1.3,versicolor"
        , "6.3,2.5,4.9,1.5,versicolor"
        , "6.1,2.8,4.7,1.2,versicolor"
        , "6.4,2.9,4.3,1.3,versicolor"
        , "6.6,3.0,4.4,1.4,versicolor"
        , "6.8,2.8,4.8,1.4,versicolor"
        , "6.7,3.0,5.0,1.7,versicolor"
        , "6.0,2.9,4.5,1.5,versicolor"
        , "5.7,2.6,3.5,1.0,versicolor"
        , "5.5,2.4,3.8,1.1,versicolor"
        , "5.5,2.4,3.7,1.0,versicolor"
        , "5.8,2.7,3.9,1.2,versicolor"
        , "6.0,2.7,5.1,1.6,versicolor"
        , "5.4,3.0,4.5,1.5,versicolor"
        , "6.0,3.4,4.5,1.6,versicolor"
        , "6.7,3.1,4.7,1.5,versicolor"
        , "6.3,2.3,4.4,1.3,versicolor"
        , "5.6,3.0,4.1,1.3,versicolor"
        , "5.5,2.5,4.0,1.3,versicolor"
        , "5.5,2.6,4.4,1.2,versicolor"
        , "6.1,3.0,4.6,1.4,versicolor"
        , "5.8,2.6,4.0,1.2,versicolor"
        , "5.0,2.3,3.3,1.0,versicolor"
        , "5.6,2.7,4.2,1.3,versicolor"
        , "5.7,3.0,4.2,1.2,versicolor"
        , "5.7,2.9,4.2,1.3,versicolor"
        , "6.2,2.9,4.3,1.3,versicolor"
        , "5.1,2.5,3.0,1.1,versicolor"
        , "5.7,2.8,4.1,1.3,versicolor"
        , "6.3,3.3,6.0,2.5,virginica"
        , "5.8,2.7,5.1,1.9,virginica"
        , "7.1,3.0,5.9,2.1,virginica"
        , "6.3,2.9,5.6,1.8,virginica"
        , "6.5,3.0,5.8,2.2,virginica"
        , "7.6,3.0,6.6,2.1,virginica"
        , "4.9,2.5,4.5,1.7,virginica"
        , "7.3,2.9,6.3,1.8,virginica"
        , "6.7,2.5,5.8,1.8,virginica"
        , "7.2,3.6,6.1,2.5,virginica"
        , "6.5,3.2,5.1,2.0,virginica"
        , "6.4,2.7,5.3,1.9,virginica"
        , "6.8,3.0,5.5,2.1,virginica"
        , "5.7,2.5,5.0,2.0,virginica"
        , "5.8,2.8,5.1,2.4,virginica"
        , "6.4,3.2,5.3,2.3,virginica"
        , "6.5,3.0,5.5,1.8,virginica"
        , "7.7,3.8,6.7,2.2,virginica"
        , "7.7,2.6,6.9,2.3,virginica"
        , "6.0,2.2,5.0,1.5,virginica"
        , "6.9,3.2,5.7,2.3,virginica"
        , "5.6,2.8,4.9,2.0,virginica"
        , "7.7,2.8,6.7,2.0,virginica"
        , "6.3,2.7,4.9,1.8,virginica"
        , "6.7,3.3,5.7,2.1,virginica"
        , "7.2,3.2,6.0,1.8,virginica"
        , "6.2,2.8,4.8,1.8,virginica"
        , "6.1,3.0,4.9,1.8,virginica"
        , "6.4,2.8,5.6,2.1,virginica"
        , "7.2,3.0,5.8,1.6,virginica"
        , "7.4,2.8,6.1,1.9,virginica"
        , "7.9,3.8,6.4,2.0,virginica"
        , "6.4,2.8,5.6,2.2,virginica"
        , "6.3,2.8,5.1,1.5,virginica"
        , "6.1,2.6,5.6,1.4,virginica"
        , "7.7,3.0,6.1,2.3,virginica"
        , "6.3,3.4,5.6,2.4,virginica"
        , "6.4,3.1,5.5,1.8,virginica"
        , "6.0,3.0,4.8,1.8,virginica"
        , "6.9,3.1,5.4,2.1,virginica"
        , "6.7,3.1,5.6,2.4,virginica"
        , "6.9,3.1,5.1,2.3,virginica"
        , "5.8,2.7,5.1,1.9,virginica"
        , "6.8,3.2,5.9,2.3,virginica"
        , "6.7,3.3,5.7,2.5,virginica"
        , "6.7,3.0,5.2,2.3,virginica"
        , "6.3,2.5,5.0,1.9,virginica"
        , "6.5,3.0,5.2,2.0,virginica"
        , "6.2,3.4,5.4,2.3,virginica"
        , "5.9,3.0,5.1,1.8,virginica"
        };
    }
}
