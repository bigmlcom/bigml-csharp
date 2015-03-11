## C# bindings for BigML.io

These bindings expose a full LINQ provider, a strongly typed
projection of all the JSON objects exposed by the REST API, as well as
the ability to compile models to .NET assemblies.

The implementation of the LINQ provider may be an interesting topic of
study by itself, and follows the pattern outlined in
[The World According to LINQ](http://queue.acm.org/detail.cfm?id=2024658).

### Adding JSON Library

This bindings uses the System.Json dll that was released as part of Silverlight
Framework ([See reference] (https://msdn.microsoft.com/en-us/library/system.json(v=vs.95).aspx)).
So, its common to add it mannually. In order to add it you should use the
package manager. In your visual studio enviroments
go to the package manager console (Tools > Library packages
manager > Package manager console) and type:
```Shell
Install-Package System.Json -Version 4.0.20126.16343 BigML
```
you should see a message like this
```Shell
'System.Json 4.0.20126.16343' was successfully added to BigML.
```

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

The model description is a JSON object that represents the decision
tree that BigML has learned from the data we fed to it. We translate
the model into a .NET expression tree and then compile the expression
tree into a .NET delegate, and call it on one of the test inputs to
see if it predicts the same kind of iris:

```c#
var description = model.ModelDescription;
Console.WriteLine(description.ToString());

// First convert it to a .NET expression tree
var expression = description.Expression();
Console.WriteLine(expression.ToString());

// Then compile the expression tree into MSIL
var predict = expression.Compile() as Func<double,double,double,double,string>;

// And try the first flower of the example set.
var result2 = predict(5.1, 3.5, 1.4, 0.2);
Console.WriteLine("result = {0}, expected = {1}", result2, "setosa");
```

## Support

Please report problems and bugs to our
[BigML.io issue tracker](https://github.com/bigmlcom/io/issues).

You can also join us in our
[Campfire chatroom](https://bigmlinc.campfirenow.com/f20a0).
