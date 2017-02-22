Projects
========

A project is an abstract resource that helps you group related BigML resources. You can assign any resource to a pre-existing project (except for projects themselves). When a user assings a source or a dataset to a project all the subsequent resources created using it will belong to the same project.

When using projects, resources will be organized in your Dashboard in project folders. If you change the project in the project selection bar the shown resources will change accordingly. To show all the resources or mix resources from different projects unselect any current project and set selector as “All”.

Creating a project
------------------

To create a project you don’t need to provide any information and BigML will give a default name to it. However, it’s recommended to set a mainingful name, description or tags to make it easy to work with.

``` {.csharp}
using BigML;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Demo
{
  /// <summary>
  /// This example creates a new project, uploads a local file that generates a
  /// source and includes it in the previosly created project.
  ///
  /// See complete API developers documentation at https://bigml.com/api
  /// </summary>
  class CreatesProjectAddSource
  {
    static async void Main()
    {
      string User = "myuser";
      string ApiKey = "80bf873537c19260370a1debf995eb57dd63cXXX";
      var client = new Client(User, ApiKey);

      // Create a new project
      Project.Arguments pArgs = new Project.Arguments();
      pArgs.Add("name", "My tests");
      Project p = await client.CreateProject(pArgs);

      string projectID = p.Resource;

      // Create a new source
      Source s = await client.CreateSource("C:/Users/files/data.csv", "Data");
      while ((s = await client.Get<Source>(s.Resource))
                              .StatusMessage.NotSuccessOrFail())
      {
        await Task.Delay(5000);
      }

      // Set the project and update the Source
      JObject changes = new JObject();
      changes["project"] = projectID;
      s = await client.Update<Source>(s.Resource, changes);
    }
  }
}
```

Every resource stored in BigML can be moved from one project to another or included in a certain project[^1]. As in the previous example, you only need to make an update operation on the resource giving the projectID. For instance, you can create a bunch of models in your “Tests” project and move the best one to your “Production” project. In the Dashboard, resources can be moved from the list view or from the detail view.

Removing a project
------------------

One of the benefits of using projects for your work is that removing a project implies removing all the resources included. Thus, you won’t need to remove them one by one.

``` {.csharp}
using BigML;
using System;
using System.Threading.Tasks;

namespace Demo
{
  /// <summary>
  /// This example removes a project
  ///
  /// See complete API developers documentation at https://bigml.com/api
  /// </summary>
  class RemovesProject
  {
    static async void Main()
    {
      string User = "myuser";
      string ApiKey = "80bf873537c19260370a1debf995eb57dd63cXXX";
      var client = new Client(User, ApiKey);

      // This is the project with my tests
      string projectID = "project/57f65df5421aa9efdf000YYY";

      // Remove the project (and its resources)
      Response rs = await client.Delete(projectID);
    }
  }
}
```

[^1]: Once a resource is included in a project, it’s not possible set it
    out of any project.
