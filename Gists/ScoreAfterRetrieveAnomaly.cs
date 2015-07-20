using BigML;
using System;
using System.Threading.Tasks;

namespace Demo
{
    class ScoreAfterRetrieveAnomaly
    {
        static async void Main()
        {
            // New BigML client with username and API key
            Console.Write("user: "); var User = Console.ReadLine();
            Console.Write("key: "); var ApiKey = Console.ReadLine();
            var client = new Client(User, ApiKey);

            // retrieve a anomaly detector with a known ID
            Anomaly anomaly;
            string anomalyId = "anomaly/54daa82eaf447f5daa000XXY"; //Put your ID here
            if ((anomaly = await client.Get<Anomaly>(anomalyId)).StatusMessage.StatusCode != Code.Finished) {
                Console.WriteLine("Error retrieving anomaly " + anomalyId);
            } else {
                Console.WriteLine(anomaly.StatusMessage.ToString());
            }

            //Input the data and calculate the score
            var parameters = new AnomalyScore.Arguments();
            parameters.Anomaly = anomaly.Resource;
            parameters.InputData.Add("000000", 7.9);
            parameters.InputData.Add("000001", 3.8);
            parameters.InputData.Add("000002", 6.4);
            parameters.InputData.Add("000003", 2);
            parameters.InputData.Add("000004", "virginica");
            AnomalyScore score;
            while ((score = await client.Create<AnomalyScore>(parameters)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
            Console.WriteLine(score.StatusMessage.ToString());

        }
    }
}