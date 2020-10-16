using NUnit.Framework;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Configuration;
using BigML;

namespace BigML.Tests
{
    [TestFixture()]
    public class TestConfigurations
    {
        string userName = ConfigurationManager.AppSettings["BIGML_USERNAME"];
        string apiKey = ConfigurationManager.AppSettings["BIGML_API_KEY"];

        [Test()]
        public async Task TestCase()
        {
            Client c = new Client(userName, apiKey);

            Configuration.Arguments cfArgs = new Configuration.Arguments();
            cfArgs.Add("name", "configuration bindings test");
            Newtonsoft.Json.Linq.JObject configs = new Newtonsoft.Json.Linq.JObject();
            Newtonsoft.Json.Linq.JObject any = new Newtonsoft.Json.Linq.JObject();
            Newtonsoft.Json.Linq.JArray tags = new Newtonsoft.Json.Linq.JArray();
            tags.Add("test");
            any.Add("tags", tags);
            configs.Add("any", any);

            cfArgs.Add("configurations", configs);
            Configuration cf = await c.CreateConfiguration(cfArgs);

            Ordered<Configuration.Filterable, Configuration.Orderable, Configuration> result
                = (from cfg in c.ListConfigurations()
                   orderby cfg.Created descending
                   select cfg);
            Listing<Configuration> configurations = await result;
            if (configurations.Meta.TotalCount == 0)
            {
                throw new System.Exception("Creation not created or not listed");
            }

            cf = await c.Update<Configuration>(cf.Resource, "renamed configuration");
            
            await c.Delete(cf);

        }
    }
}