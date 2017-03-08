## C# bindings for BigML.io

These bindings expose a full LINQ provider, a strongly typed
projection of all the JSON objects exposed by the REST API, as well as
the ability to compile models to .NET assemblies.

The implementation of the LINQ provider may be an interesting topic of
study by itself, and follows the pattern outlined in
[The World According to LINQ](http://queue.acm.org/detail.cfm?id=2024658).

### Adding JSON Library

This bindings uses the Newtonsoft.Json dll
([See reference] (https://www.nuget.org/packages/Newtonsoft.Json/9.0.1)).
So, its common to add it mannually. In order to add it you should use the
package manager. In your visual studio enviroments
go to the package manager console (Tools > Library packages
manager > Package manager console) and type:
```Shell
Install-Package Newtonsoft.Json -Version 9.0.1 BigML
```
you should see a message like this
```Shell
'Newtonsoft.Json 9.0.1' was successfully added to BigML.
```
The NuGet of this library is available at https://www.nuget.org/packages/BigML/ .
Last released version is 2.0.0. Previous versions (<2.0) use Microsoft's
System.Json deprecated package and we encourage to update to a 2.0 version or higher.


### Accessing BigML.io

To access BigML using the bindings, you first create a new client
object by passing your user name and API key. The client object
provides methods for most of the operations provided by
[the BigML API](https://bigml.com/developers) (of course the binding
may not reflect all the latest features, for example we do not
implement Evaluations yet, but that is why we provide the source on
GitHub) such as listing, filtering, and sorting your sources using
LINQ queries.  For instance:

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

### Creating a datasources, datasets and models

Once we have printed out the existing sources, we can create a new
source from an in-memory collection, but BigML (and the .NET bindings)
also supports creating sources from local files, Amazon S3, or Azure
Blob store. And from that a dataset, and a model. Since it can take a
while for the BigML service to process creation of sources, datasets,
and models, we need to poll until we get status code “finished” back
from the service:

```c#
// New source from in-memory stream, with separate header.
var source = await client.Create(iris, "Iris.csv", "sepal length, sepal width, petal length, petal width, species");
// No push, so we need to busy wait for the source to be processed.
while ((source = await client.Get(source)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
Console.WriteLine(source.StatusMessage.ToString());

// Default dataset from source
var dataset = await client.Create(source);
// No push, so we need to busy wait for the source to be processed.
while ((dataset = await client.Get(dataset)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
Console.WriteLine(dataset.StatusMessage.ToString());

// Default model from dataset
var model = await client.Create(dataset);
// No push, so we need to busy wait for the source to be processed.
while ((model = await client.Get(model)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
Console.WriteLine(model.StatusMessage.ToString());
```

### Manipulating models

The model `root` property is a JSON object that represents the decision
tree that BigML has learned from the data we fed to it. We translate
the model into a Nodes tree in memory, and call it on one of the test 
inputs to see if it predicts the same kind of iris:

```c#
// Transforms JSON in tree structure 
Model.LocalModel localModel = model.ModelStructure;

// --- Specify prediction inputs and calculate the prediction ---
// input data can be provided by fieldID or by name
Dictionary<string, dynamic> inputData = new Dictionary<string, dynamic>();
inputData.InputData.Add("sepal width", 5);
inputData.InputData.Add("00003", 2.5);
// Other values are ommited or unknown
Model.Node prediction = localModel.predict(inputData);

Console.WriteLine("result = {0}, expected = {1}", prediction.Output, "setosa");
```

## Support

Please report problems and bugs to our
[BigML.io issue tracker](https://github.com/bigmlcom/io/issues).

You can also join us in our
[Campfire chatroom](https://bigmlinc.campfirenow.com/f20a0).
