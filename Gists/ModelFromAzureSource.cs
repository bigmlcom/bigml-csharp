using BigML;
using System;
using System.Threading.Tasks;

namespace Demo
{
    class ModelFromAzureSource
    {
        static async void Main()
        {
            // New BigML client with username and API key
            Console.Write("user: "); var User = Console.ReadLine();
            Console.Write("key: "); var ApiKey = Console.ReadLine();
            var client = new Client(User, ApiKey);

            // Create a source from a file in azure storage
            var remoteURL = @"azure://csv/iris.csv?AccountName=bigmlpublic";
            Source.Arguments remoteArguments = new Source.Arguments();
            remoteArguments.Remote = remoteURL;
            remoteArguments.Name = "Iris from Azure";
            var sourceFromAzure = await client.Create(remoteArguments);
            while ((sourceFromAzure = await client.Get(sourceFromAzure)).StatusMessage.NotSuccessOrFail()) await Task.Delay(10);

            // Create a dataset from this sources
            var dataset = await client.Create(sourceFromAzure);
            while ((dataset = await client.Get(dataset)).StatusMessage.NotSuccessOrFail()) await Task.Delay(10);
            Console.WriteLine(dataset.StatusMessage.ToString());

            // Default model from this dataset
            var model = await client.Create(dataset);
            // No push, so we need to busy wait for the source to be processed.
            while ((model = await client.Get(model)).StatusMessage.NotSuccessOrFail()) await Task.Delay(10);
            Console.WriteLine(model.StatusMessage.ToString());
        }
    }
}