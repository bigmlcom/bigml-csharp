Preparing your data
===================

BigML resources are immutable, except some helpful properties: name, description, category and tags. For sources and datasets contained data are still immutable, but you may need to change some properties to ensure your data is correctly handled. 

Updating a source
-----------------

Sources describe the structure inferred by BigML from the uploaded data. This structure includes field names and types, locale, missing values, etc. You can learn more about all the available source attributes at <https://bigml.com/api/sources\#sr_source_properties>. Some of them can be updated to ensure that your data is correctly interpreted. For instance, you could upload a source where a column contains only a subset of integers. BigML will consider this a numeric field. However, in your domain these integers could be the code associated to a category. Then, the field should be handled like a categorical field. You can change the type assigned to field by calling:

``` {.csharp}
using BigML;
using System;
using System.Threading.Tasks;
using System.Json;

namespace Demo
{
    /// <summary>
    /// Updates a source stored in BigML.
    ///
    /// See complete Sources documentation at
    /// https://bigml.com/api/sources
    /// </summary>
    class UpdateSource
    {
        static async void Main()
        {
            // New BigML client in production mode with username and API key
            Console.Write("user: ");
            var User = Console.ReadLine();
            Console.Write("key: ");
            var ApiKey = Console.ReadLine();
            var client = new Client(User, ApiKey);

            // change the id to your source
            string sourceId = "source/57d7240228eb3e69f3000XXX";

            dynamic data = new JsonObject();
            data["fields"] = new JsonObject();
            data["fields"]["000000"] = new JsonObject();
            data["fields"]["000000"].optype = OpType.Categorical.ToString().ToLower();

            // Apply changes
            client.Update<Source>(sourceId, data);
        }
    }
}
```

where `sourceId` is the variable that contains the ID of the source to be updated and we change the type of the field whose ID is `000000` to categorical. The IDs for the fields can be found in the `fields` attribute of the the source structure, which contains the description of each field keyed by its ID.

Updating a dataset
------------------

Datasets are the starting point for all models in BigML and contain a serialized version of your data where each field has been summarized and some basic statistics computed. According to its contents, each field gets an attribute called `preferred` whose value is a boolean. The value is set to `true` when BigML thinks that this field will be useful as predictor when creating a model. Fields like IDs, constants or with unique values are marked as `preferred = false` as they are not usually useful when modeling. However, this attribute can be changed if you still want to include it as predictor for the model.

Another commonly updated attribute in a dataset is the `objective_field`, that is, the field to be predicted in decision trees, ensembles or logistic regressions. By default, the last field in your dataset is used as objective field. The following example shows how to update the objective field to the first field in your dataset, whose ID is `000000`, and to include or exclude several fields from further analysis by changing their `preferred` attribute.

``` {.csharp}
using BigML;
using System;
using System.Threading.Tasks;
using System.Json;

namespace Demo
{
    /// <summary>
    /// Update some properties of a Dataset stored in BigML
    ///  * Change objective field
    ///  * Mark one field as preferred
    ///  * Exclude one field
    ///
    /// See complete Scripts and executions documentation at
    /// https://bigml.com/api/datasets
    /// </summary>
    class UpdateDataset
    {
        static async void Main()
        {
            // New BigML client in production mode with username and API key
            Console.Write("user: "); var User = Console.ReadLine();
            Console.Write("key: "); var ApiKey = Console.ReadLine();
            var client = new Client(User, ApiKey);

            // Update this string with your dataset Id
            string datasetId = "dataset/57d7283b28eb3e69f1000XXX";

            dynamic data = new JsonObject();
            data["fields"] = new JsonObject();
            // Mark one field as preferred
            data["fields"]["000000"] = new JsonObject();
            data["fields"]["000000"].preferred = true;
            // Exclude these two fields
            data["fields"]["00003f"] = new JsonObject();
            data["fields"]["00003f"].preferred = false;
            data["fields"]["000041"] = new JsonObject();
            data["fields"]["000041"].preferred = false;
            // Update Objective field
            data["objective_field"] = new JsonObject();
            data["objective_field"]["id"] = "000003";

            // Apply changes
            client.Update<DataSet>(datasetId, data);
        }
    }
}
```
