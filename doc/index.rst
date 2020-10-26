.. toctree::
   :maxdepth: 2
   :hidden:

   update_source_and_dataset
   batch_and_local_prediction
   list_filter_and_update
   projects
   whizzml
   contribute


BigML C# Bindings
=================
In this tutorial, you will learn how to use the BigML bindings for C sharp.

Additional Information
----------------------

For additional information about the API, see the
`BigML developer's documentation <https://bigml.com/api>`_.

Introduction
============

Requirements and installation
-----------------------------

BigML C# bindings use the `Newtonsoft.Json` DLL that was released as part of
.NET Silverlight Framework, and you will need to explicitly install it in your
system, if you have not already.

To install `Newtonsoft.Json` you can use Visual Studio Package Manager.
In your Visual Studio IDE, go to the Package Manager console
(Tools > Library Package Manager > Package Manager Console) and type
the following command:

.. code-block:: shell-session

    Install-Package Newtonsoft.Json -Version 12.0.3

If the installation is successful, you should see a message like the
following one:

.. code-block:: shell-session

    'Newtonsoft.Json 12.0.3' was successfully added to <your project
    name here>.

Once you have the `Newtonsoft.Json` package installed, you can use Visual
Studio Package Manager to install BigML C# bindings:

.. code-block:: shell-session

    Install-Package BigML


Authentication
--------------

To access BigML using the bindings, you first create a new `client` object
by passing your user name and API Key. This is how you can initialize a new
`Client` object by retrieving user name and API Key from your standard
input:

.. code-block:: C#

    // New BigML client using user name and API key.
    Console.Write("user: ");
    var user = Console.ReadLine();
    Console.Write("key: ");
    var apiKey = Console.ReadLine();
    var client = new Client(user, apiKey);


The `client` object encapsulates your credentials and provides
methods for most of the operations available through the BigML API [#f1]_ .


Connecting to a Virtual Private Cloud
-------------------------------------

If you are using
`Managed Virtual Private Cloud <https://support.bigml.com/hc/en-us/articles/208270815-What-is-a-Managed-VPC->`_ (VPC),
you can specify your VPC URL when instantiating your `client`:

.. code-block:: C#

    var client = new Client(userName, apiKey, vpcDomain: "yourVPC.vpc.bigml.io");


BigML VPC provides transparent, exclusive access to resizable
computing and data storage capacity in the cloud without needing to install
or configure any hardware or software.

Getting started
===============

This chapter shows how to create a model from a remote CSV file and use it to
make a prediction for a new single instance.

Imagine that you want to use a **remote CSV file**
<https://static.bigml.com/csv/iris.csv> containing the
`Iris flower dataset <http://en.wikipedia.org/wiki/Iris\_flower\_data\_set>`_
to predict the species of a flower based on its morphological characteristics.
A preview of the dataset is shown below. It has 4 numeric fields: sepal length,
sepal width, petal length, petal width and a categorical field: species.
By default, BigML considers the last field in the dataset as the objective
field (i.e., the field you want to predict).

.. code-block:: shell-session

    sepal length,sepal width,petal length,petal
    width,species
    5.1,3.5,1.4,0.2,Iris-setosa 4.9,3.0,1.4,0.2,Iris-setosa
    4.7,3.2,1.3,0.2,Iris-setosa 5.8,2.7,3.9,1.2,Iris-versicolor
    6.0,2.7,5.1,1.6,Iris-versicolor 5.4,3.0,4.5,1.5,Iris-versicolor
    6.8,3.0,5.5,2.1,Iris-virginica 5.7,2.5,5.0,2.0,Iris-virginica
    5.8,2.8,5.1,2.4,Iris-virginica
    ...


The typical process you need to follow when using BigML is to:

1.  open a connection to BigML API with your user name and API Key

2.  create a **source** by uploading the data file

3.  create a **dataset** (a structured version of the source)

4.  create a **model** using the dataset

5.  finally, use the model to make a **prediction** for some new input data.

As you can see, all the steps above share some similarities, in that
each one consists of creating a new BigML resource from some other BigML
resource. This makes the BigML API very easy to understand and use,
since all available operations are orthogonal to the kind of resource
you want to create.

All API calls in BigML are asynchronous, so you will not be blocking
your program while waiting for the network to send back a reply.
This means that at each step you need to wait for the resource creation
to finish before you can move on to the next step.

This can be exemplified with the first step in our process,
creating a **source** by uploading the data file.

First of all, you will need to create a `Source` object to encapsulate
all information that will be used to create it correctly, i.e., an optional
name for the source and the data file to use:

.. code-block:: C#

    var parameters = new Source.Arguments();
    parameters.Add("name", "my new source");
    parameters.Add("remote",
       "https://static.bigml.com/csv/iris.csv");
    Source source = await client.CreateSource(parameters);


If you do not want to use a remote data file, as you are doing in this example,
you can use a local data file by replacing the last line above, as shown here:

.. code-block:: C#

    var filePath = "./iris.csv";
    string name = "Iris file";
    Source source = await client.CreateSource(filePath, name);


That’s all! BigML will create the source, as per our request, and
automatically list it in the BigML Dashboard. As mentioned, though, you will
need to monitor the source status until it is fully created before you can
move on to the next step, which can be easily done like this:

.. code-block:: C#
    while ((source = await client.Get<Source>(source))
               .StatusMessage
               .NotSuccessOrFail())
    {
        await Task.Delay(5000);
    }


The steps described above define a generic pattern of how to create the
resources you need next, i.e., a `Dataset`, a `Model`, and a `Prediction`.
As an additional example, this is how you create a `Dataset` from the `Source`
you have just created:

.. code-block:: C#

    // --- create a dataset from the previous source ---
    // Dataset object which will encapsulate the dataset information
    Dataset dataset;
    // setting the parameters to be used in dataset creation
    var parameters = new Dataset.Arguments();
    parameters.Add("name", "my new dataset");
    // using the source ID as argument
    parameters.Add("source", source.Resource);
    dataset = await client.CreateDataset(parameters);
    // checking the dataset status
    while ((dataset = await client.Get<Dataset>(dataset))
                                .StatusMessage
                                .NotSuccessOrFail())
    {
    await Task.Delay(5000);
    }

After this quick introduction, it should be now easy to follow and understand
the full code that is required to create a prediction starting from a data
file. Make sure you have properly installed BigML C# bindings
as detailed in [Requirements and installation](#requirements-and-installation).

.. code-block:: C#

    using BigML;
    using System;
    using System.Threading.Tasks;

    namespace Demo
    {
      /// <summary>
      /// This example creates a prediction using a model created with the data
      /// stored in a remote file.
      ///
      /// See complete API developers documentation at https://bigml.com/api
      /// </summary>
      class CreatesPrediction
      {
        static async void Main()
        {
          // --- New BigML client using user name and API key ---
          Console.Write("user: ");
          var user = Console.ReadLine();
          Console.Write("key: ");
          var apiKey = Console.ReadLine();
          var client = new Client(user, apiKey);

          // --- create a source from the data in a remote file ---

          // setting the parameters to be used in source creation
          var parameters = new Source.Arguments();
          parameters.Add("name", "my new source");
          // uploading a remote file
          parameters.Add("remote", "https://static.bigml.com/csv/iris.csv");
          // if you need to upload a local file, change last line to
          // parameters.Add("file", "iris.csv");
          // Source object which will encapsulate the source information
          Source source = await client.CreateSource(parameters);
          // API calls are asynchronous, so you need to check that the source is finally
          // finished. To learn about the possible states for
          // BigML resources, please see http://bigml.com/api/status_codes
          while ((source = await client.Get<Source>(source))
                                       .StatusMessage
                                       .NotSuccessOrFail())
          {
            await Task.Delay(5000);
          }

          // --- create a dataset from the previous source ---
          // setting the parameters to be used in dataset creation
          var parameters = new Dataset.Arguments();
          parameters.Add("name", "my new dataset");
          // using the source ID as argument
          parameters.Add("source", source.Resource);
          // Dataset object which will encapsulate the dataset information
          Dataset dataset = await client.CreateDataset(parameters);
          // checking the dataset status
          while ((dataset = await client.Get<Dataset>(dataset))
                                        .StatusMessage
                                        .NotSuccessOrFail())
          {
            await Task.Delay(5000);
          }

          // --- create a model from the previous dataset ---
          // setting the parameters to be used in model creation
          var parameters = new Model.Arguments();
          parameters.Add("name", "my new model");
          // using the dataset ID as argument
          parameters.Add("dataset", dataset.Resource);
          // Model object which will encapsulate the model information
          Model model = await client.CreateModel(parameters);
          // checking the model status
          while ((model = await client.Get<Model>(model))
                                      .StatusMessage
                                      .NotSuccessOrFail())
          {
            await Task.Delay(5000);
          }

          // --- create a prediction using the model ---
          // setting the parameters to be used in prediction creation
          var parameters = new Prediction.Arguments();
          // using the model ID as argument
          parameters.Add("model", model.Resource);
          // set INPUT DATA for prediction: {'petal length': 5, 'sepal width': 2.5}
          parameters.InputData.Add("petal length", 5);
          parameters.InputData.Add("sepal width", 2.5);

          // SET MISSING STRATEGY and NAME
          parameters.Add("missing_strategy", 1); //Proportional
          parameters.Add("name", "prediction w/ PROPORTIONAL");
          // Prediction object which will encapsulate the prediction information
          Prediction prediction = await client.CreatePrediction(parameters);
          // checking the prediction status
          while ((prediction = await client.Get<Prediction>(prediction))
                                           .StatusMessage
                                           .NotSuccessOrFail())
          {
              await Task.Delay(2000);
          }
          Console.WriteLine("------------------------------\nMissing strategy PROPORTIONAL");
          Console.WriteLine("Prediction: " + prediction.GetPredictionOutcome<string>());
          Console.WriteLine("Confidence: " + prediction.Confidence);


          // Test same input_data, but with missing_stategy = 0 (default value)
          // UPDATE MISSING STRATEGY and NAME
          parameters.Update("missing_strategy", 0); //Last prediction
          parameters.Update("name", "prediction w/ LAST PREDICTION");
          prediction = await client.CreatePrediction(parameters);
          while ((prediction = await client.Get<Prediction>(prediction))
                                           .StatusMessage
                                           .NotSuccessOrFail())
          {
              await Task.Delay(2000);
          }

          Console.WriteLine("------------------------------\nMissing strat. LAST PREDICTION");
          Console.WriteLine("Prediction: " + prediction.GetPredictionOutcome<string>());
          Console.WriteLine("Confidence: " + prediction.Confidence);
          Console.WriteLine("------------------------------");
        }
      }
    }


.. rubric:: Footnotes

.. [#f1] You can find your API Key in your BigML account information panel.
         If needed, you can also create additional API Keys and restrict the
         privileges that are associated with each of them.
