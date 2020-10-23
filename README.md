## C# bindings for BigML.io

These bindings expose a full LINQ provider, a strongly typed
projection of all the JSON objects exposed by the REST API, as well as
the ability to compile models to .NET assemblies.

The implementation of the LINQ provider may be an interesting topic of
study by itself, and follows the pattern outlined in
[The World According to LINQ](http://queue.acm.org/detail.cfm?id=2024658).

### Adding JSON Library

This bindings uses the Newtonsoft.Json dll
([See reference](https://www.nuget.org/packages/Newtonsoft.Json/12.0.3)).
So, its common to add it mannually. In order to add it you should use the
package manager. In your visual studio enviroments
go to the package manager console (Tools > Library packages
manager > Package manager console) and type:
```Shell
Install-Package Newtonsoft.Json -Version 12.0.3 BigML
```
you should see a message like this
```Shell
'Newtonsoft.Json 12.0.3' was successfully added to BigML.
```
The NuGet of this library is available at https://www.nuget.org/packages/BigML/ .
Last released version is 2.5.1. Previous versions (<2.0) use Microsoft's
System.Json deprecated package and we encourage to update to a 2.0
version or higher.


### Accessing BigML.io

To access BigML using the bindings, you first create a new client
object by passing your user name and API key. The client object
provides methods for most of the operations provided by
[the BigML API](https://bigml.com/api) such as listing,
filtering, and sorting your resources using
LINQ queries.  For instance, to list the sources in your account:

```c#
// New BigML client using username and API key.
Console.Write("user: "); var User = Console.ReadLine();
Console.Write("key: "); var ApiKey = Console.ReadLine();
var client = new Client(User, ApiKey);

Ordered<Source.Filterable, Source.Orderable, Source> result
      = (from s in client.ListSources()
	     orderby s.Created descending
	     select s);

var sources = await result;
foreach(var src in sources) Console.WriteLine(src.ToString());
```

### Modeling steps and resources

The C# bindings allow you to create resources in BigML. The first resource that
you will need to create is a `source`- Sources can be created
from an in-memory collection. BigML (and the .NET bindings)
also supports creating sources from local files, Amazon S3, or Azure
Blob store. Creating a resource is an asynchronous operation that can take a
while. We'll need to repeatedly poll until we get the resource in a
`finished` status code.

```c#
// New source from in-memory stream, with separate header.
var source = await client.Create(iris, "Iris.csv", "sepal length, sepal width, petal length, petal width, species");
// No push, so we need to busy wait for the source to be processed.
while ((source = await client.Get(source)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
Console.WriteLine(source.StatusMessage.ToString());
```

The following step towards modeling would be creating a dataset. Datasets
summarize the information in your data.

```c#
// Default dataset from source
var dataset = await client.Create(source);
// No push, so we need to busy wait for the source to be processed.
while ((dataset = await client.Get(dataset)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
Console.WriteLine(dataset.StatusMessage.ToString());
```
And now we would be ready to create a model from the dataset.

```c#
// Default model from dataset
var model = await client.Create(dataset);
// No push, so we need to busy wait for the source to be processed.
while ((model = await client.Get(model)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
Console.WriteLine(model.StatusMessage.ToString());
```

### Using models locally

Models, like the rest of resources in BigML, are downloadable JSON objects.
Each type of model has a particular scheme whose properties are described in
the corresponding [API documentation](https://bigml.com/api)

Decision trees, for instance, contain a `root` property which
is a JSON object that represents the rules that the algorithm has found
in your data. The `ModelStructure` method of the `Model` class
maps these rules into a tree of `Nodes` in memory. That is the
`Model.LocalModel`. Given a new test input, you can use the local model to
run the input through the rules and produce a prediction for the
value of the model's target field (the `objective field`):

```c#
// Transforms JSON in tree structure
Model.LocalModel localModel = model.ModelStructure();

// --- Specify prediction inputs and calculate the prediction ---
// input data can be provided by fieldID or by name
Dictionary<string, dynamic> inputData = new Dictionary<string, dynamic>();
inputData.Add("sepal width", 5);
inputData.Add("00003", 2.5);
// Other values are ommited or unknown
Model.Node prediction = localModel.predict(inputData);

Console.WriteLine("result = {0}, expected = {1}", prediction.Output, "setosa");
```

The same can be done for `ensembles` using the `EnsembleStructure` method
of the `Ensemble` class to create a `LocalEnsemble`.

## Development and Testing

The `BigML.Tests` project in the repo contains some tests that can be run to
ensure the bindings CRUD functionality. In order to connect to `BigML` you will
need to set your credentials in `BigML` as properties in the `app.config`
file.

The `Iris` project in the repo can be run on an external shell and will ask
for your credentials in the command line prompt. It also tests a basic
model creation workflow.

The `WFormApp`project in the repo creates a form with two buttons that retrieve
the list of sources in your environement and shows the number of them by using
two different methods. In order to work, you need to set your credentials as
environment variables: `BIGML_USERNAME` and `BIGML_API_KEY`.

Also, the `Gists` directory contains some examples of utilities to create
local and remote predictions. All of them ask for your credentials using the
command prompt.


## Support

Please, refer to the information in [Read the Docs](https://bigml-csharp.readthedocs.io/en/latest/)
to understand the use of these bindings.

Please, report problems and bugs to our
[BigML.io issue tracker](https://github.com/bigmlcom/io/issues).

You can also join us in our
[Campfire chatroom](https://bigmlinc.campfirenow.com/f20a0).
