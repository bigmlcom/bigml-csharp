Batch predictions
=================

This chapter builds [Quickstart](./intro.md) on and shows how to create a **batch prediction** for multiple instances using an existing **ensemble**. Using an `ensemble` instead on a `model` is just a variation so you can see how flexible and orthogonal BigML API is.

To create a `Batch prediction`, you will need an `ensemble` and a `dataset` containing all the data instances you want predictions for. You can easily create both resources by following the steps detailed in [Quickstart](./intro.md). In the following code, you will only focus on the last step in the process, i.e., creating the `Batch prediction`.

``` {.csharp}
using BigML;
using System;
using System.Threading.Tasks;

namespace Demo
{
  /// <summary>
  /// This example creates a batch prediction using a dataset and an ensemble
  ///  stored in BigML.
  ///
  /// See complete API developers documentation at https://bigml.com/api
  /// </summary>
  class CreatesBatchPrediction
  {
    static async void Main()
    {

      // --- New BigML client using user name and API key ---
      Console.Write("user: ");
      var user = Console.ReadLine();
      Console.Write("key: ");
      var apiKey = Console.ReadLine();
      var client = new Client(user, apiKey);

      // --- Create a Batch Prediction from a previously created ensemble ---
      // The ensemble id and the dataset id which will be used to create a batch
      // prediction.
      string modelId = "ensemble/54ad6d0558a27e2ddf000XXX";
      string datasetId = "dataset/54ad6d0558a27e2ddf000YYY";

      // Batch prediction object which will encapsulate all required information
      BatchPrediction batchPrediction;
      // setting the parameters to be used in the batch prediction creation
      var parameters = new BatchPrediction.Arguments();
      // the "model" parameter can be a Model, an Ensemble or a Logisticregression
      parameters.Add("model", modelId);
      parameters.Add("dataset", datasetId);
      // optionally, BigML can create a dataset with all results
      parameters.Add("output_dataset", true);
      // start the remote operation
      batchPrediction = await client.CreateBatchPrediction(parameters);
      string batchId = batchPrediction.Resource;
      // wait for the batch prediction to be created
      while ((batchPrediction = await client.Get<BatchPrediction>(batchId))
                                            .StatusMessage
                                            .NotSuccessOrFail())
      {
        await Task.Delay(10);
      }
      Console.WriteLine(batchPrediction.OutputDatasetResource);
    }
  }
}
```

In the code above, if you want to use a `model` or a `logistic regression` instead of an `ensemble`, all you have to do is specify the model’s or logistic regression’s Id for the “model” parameter.

Local predictions
=================
In addition to making predictions remotely on BigML servers, the BigML C# bindings also provide support for making predictions locally. This means you can download your `model`, `ensemble`, or `logistic regression` resource and use it to make predictions offline, i.e., without accessing the network.

The following code snippet shows how you can retrieve a `model` created BigML in and use it for local predictions:

``` {.csharp}
using BigML;
using System;
using System.Threading.Tasks;

namespace Demo
{
  /// <summary>
  /// This example retrieves a model previously created in BigML and uses
  /// it to make a prediction locally.
  ///
  /// See complete API developers documentation at https://bigml.com/api
  /// </summary>
  class RetrieveModelPredictLocally
  {
    static async void Main()
    {
      // --- New BigML client using user name and API key ---
      Console.Write("user: ");
      var User = Console.ReadLine();
      Console.Write("key: ");
      var ApiKey = Console.ReadLine();
      var client = new Client(User, ApiKey);

      // --- Retrieve an existing model whose Id is known ---
      Model model;
      string modelId = "model/575085112275c16672016XXX";
      while ((model = await client.Get<Model>(modelId))
                                  .StatusMessage
                                  .StatusCode != Code.Finished)
      {
        await Task.Delay(10);
      }
      Model.LocalModel localModel = model.ModelStructure;

      // --- Specify prediction inputs and calculate the prediction ---
      Dictionary<string, dynamic>
      inputData = new Dictionary<string, dynamic>();
      inputData.InputData.Add("sepal length", 5);
      inputData.InputData.Add("sepal width", 2.5);
      Model.Node prediction = localModel.predict(inputData);
    }
  }
}
```
