using BigML;
using System;
using System.Threading.Tasks;

namespace Demo
{
    class PredictionWithIrisEnsemble
    {
        static async void Main()
        {
            // New BigML client in production mode with username and API key
            Console.Write("user: "); var User = Console.ReadLine();
            Console.Write("key: "); var ApiKey = Console.ReadLine();
            var client = new Client(User, ApiKey);

            var parameters = new Prediction.Arguments();
            parameters.Add("ensemble", "ensemble/54ad6d0558a27e2ddf000XXX"); //put your Id here
            parameters.InputData.Add("000000", 7.9);
            parameters.InputData.Add("000001", 3.8);
            parameters.InputData.Add("000002", 6.4);
            parameters.InputData.Add("000003", 2);
            Prediction ensemblePrediction = await client.CreatePrediction(parameters);
            Console.WriteLine(ensemblePrediction.GetPredictionOutcome<string>()); //output is an string for categorical models
        }
    }
}
