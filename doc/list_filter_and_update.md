Resources management
====================

You can list BigML’s resources using the client listing functions. In
addition using [`LINQ`](https://msdn.microsoft.com/en-us/library/bb397676.aspx)
xpressions you can retrieve, filter and order a collection of them.

Filtering resources
-------------------

In order to filter resources you can use any of the properties labeled
as *filterable* in the [BigML documentation](https://bigml.com/api). Please,
check the available properties for each
kind of resource in their particular section. In addition to specific
selectors you can use two general selectors to paginate the resources
list: `offset` and `limit`. For details, please check
[this requests section](https://bigml.com/api/requests#rq_paginating_resources).

``` {.csharp}
  string User = "myuser";
  string ApiKey = "80bf873537c19260370a1debf995eb57dd63cXXX";
  var client = new Client(User, ApiKey);

  // --- Select sources starting at tenth position
  Ordered<Source.Filterable, Source.Orderable, Source> result
        = (from s in client.ListSources()
          where s.Offset == 10
          select s);

  // Wait for the results
  var sources = await result;

  foreach (var src in sources)
  {
    // print results
    Console.WriteLine(src.ToString());
  }
```

Ordering resources
------------------

In order to sort resources you can use any of the properties labeled as
*sortable* in the [BigML documentation](https://bigml.com/api). Please,
check the sortable properties for each kind
of resources in their particular section. By default BigML paginates the
results in groups of 20, so it’s possible that you need to specify the
`offset` or increase the `limit` of resources to returned in the list
call. For details, please, check
[this requests section](https://bigml.com/api/requests#rq_paginating_resources).

``` {.csharp}
  string User = "myuser";
  string ApiKey = "80bf873537c19260370a1debf995eb57dd63cXXX";
  var client = new Client(User, ApiKey);

  // --- Select 50 sources and order by creation date
  // from more recent to less recent
  Ordered<Source.Filterable, Source.Orderable, Source> result
        = (from s in client.ListSources()
          where s.Limit == 50
          orderby s.Created descending
          select s);

  // Wait for the results from the server
  var sources = await result;

  foreach (var src in sources)
  {
    // do work with each resource: e.g. print results
    Console.WriteLine(src.ToString());
  }
```

Updating resources
------------------

In general, the core information in BigML resources is immutable. This is so
to ensure traceability and reproducibility. Therefore, only non-essential
properties that are available as auxiliar data, like `name`, `description`,
`tags` and `category`, can be modified. They are listed in the
[API help section](https://bigml.com/api).

The following is an example about how to update the tags in a model. The
`Update` method in the `Client` class receives as arguments the ID of the
resource that is to be updated and the properties that should be changed.
The ID of a resource follow the schema
`resource_type/alphanumeric24digits00ID` and can be found in the `Resource`
method of the corresponding class and is the final part of the URL that
points to the resource view in the Dashboard.


``` {.csharp}
using BigML;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Demo
{
  /// <summary>
  /// This example updates a Model previously created.
  ///
  /// See complete API developers documentation at https://bigml.com/api
  /// </summary>
  class UpdatesModelTags
  {
    static async void Main()
    {
      string User = "myuser";
      string ApiKey = "80bf873537c19260370a1debf995eb57dd63cXXX";
      var client = new Client(User, ApiKey);

      // Get the Model
      Model m;
      string modelId = "model/57f65df5421aa9efdf000YYY";
      while ((m = await client.Get<Model>(modelId))
                              .StatusMessage.NotSuccessOrFail())
      {
        await Task.Delay(5000);
      }

      // Set the tags and update the Model
      JArray tags = new JArray();
      tags.Add("cool");
      tags.Add("production");
      JObject changes = new JObject();
      changes["tags"] = tags;
      m = await client.Update<Model>(m.Resource, changes);
    }
  }
}
```

Shareable resources allow you to
modify their `privacy` and for resources included
in projects the `project` is also updatable. Due to their special nature,
some resources like `Source` or `Script` have
other properties that can be updated, e.g. the locale used in `source_parser`
of a source or `inputs` types or descriptions of the arguments for a script.
Other that that, if you want to modify a
property of your existing resource you will need to create a new one.
That's for instance the case when you want to add data to a previously created
model. You just upload your new data to create a new `source`, generate a
`dataset` from it and merge it with the existing dataset that contained the
old data. The consolidated dataset can now be used to rebuild a new `model`
based on the entire data set.


Removing resources
------------------

All the resources stored in BigML can be removed individually using
their ID. All BigML resources IDs follow the schema
`resource_type/alphanumeric24digits00ID` and this is the only required
argument of the `client.Delete` function.

``` {.csharp}
  // Remove a known model
  string resourceID = "model/57f65df5421aa9efdf000YYY"
  Response rs = await client.Delete(resourceID);
```
