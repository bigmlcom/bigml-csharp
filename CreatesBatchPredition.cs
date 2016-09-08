using BigML;
using System;
using System.Threading.Tasks;

/*
 * See complete BatchPredictions documentation at
 * https://bigml.com/api/batch_predictions
 */
namespace Demo
{
    class CreatesBatchPrediction
    {
        static async void Main()
        {
            // New BigML client in production mode with username and API key
            Console.Write("user: "); var User = Console.ReadLine();
            Console.Write("key: "); var ApiKey = Console.ReadLine();
            var client = new Client(User, ApiKey);

            // change the id to your model, ensemble or logisticregression
            string modelId = "ensemble/54ad6d0558a27e2ddf000XXX";
            string datasetId = "dataset/54ad6d0558a27e2ddf000YYY";

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
        }
    }
}