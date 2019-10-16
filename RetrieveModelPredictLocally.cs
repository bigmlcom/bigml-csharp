using BigML;
using System;
using System.Threading.Tasks;

namespace Demo
{
    class RetrieveModelPredictLocally
    {
        static async void Main()
        {
            // New BigML client with username and API key
            Console.Write("user: "); var User = Console.ReadLine();
            Console.Write("key: "); var ApiKey = Console.ReadLine();
            var client = new Client(User, ApiKey);

            Model model;
            string modelId = "model/575085112275c16672016XXX";
            while ((model = await client.Get<Model>(modelId)).StatusMessage.StatusCode != Code.Finished) {
                await Task.Delay(3000);
            }
            Model.LocalModel localModel = model.ModelStructure();

            Dictionary<string, dynamic> inputData = new Dictionary<string, dynamic>();
            inputData.Add("000000", 1.6);
            inputData.Add("000002", 5);
            inputData.Add("000003", 1.6);
            Model.Node prediction = localModel.predict(inputData);

        }
    }
}
