using BigML;
using System;
using System.Threading.Tasks;

namespace Demo
{
    class DevClusterFromAzureSource
    {
        static async void Main()
        {
            // New BigML client with username and API key
            Console.Write("user: "); var User = Console.ReadLine();
            Console.Write("key: "); var ApiKey = Console.ReadLine();
            // set true the development mode
            var client = new Client(User, ApiKey, true);

            // create a source
            var remoteURL = @"azure://csv/iris.csv?AccountName=bigmlpublic";
            Source.Arguments remoteArguments = new Source.Arguments();
            remoteArguments.Add("remote", remoteURL);
            var sourceFromAzure = await client.Create(remoteArguments);
            while ((sourceFromAzure = await client.Get(sourceFromAzure)).StatusMessage.NotSuccessOrFail()) await Task.Delay(10);
            Console.WriteLine(sourceFromAzure.StatusMessage.ToString());

            var dataset = await client.Create(sourceFromAzure);
            // No push, so we need to busy wait for the dataset to be processed.
            while ((dataset = await client.Get(dataset)).StatusMessage.NotSuccessOrFail()) await Task.Delay(10);
            Console.WriteLine(dataset.StatusMessage.ToString());

            // Customized cluster from dataset. 3 is the desired number of cluster. Sets cluster name: "my cluster"
            var cluster = await client.CreateCluster(dataset, "my cluster", 3);
            while ((cluster = await client.Get(cluster)).StatusMessage.NotSuccessOrFail()) await Task.Delay(10);
            Console.WriteLine(cluster.StatusMessage.ToString());
        }
    }
}