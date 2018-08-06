using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using BigML;

namespace BigMLTest
{
    /// <summary>
    /// Test resources related with Configurations
    /// </summary>
    [TestClass]
    public class TestConfigurations
    {
        string userName = "myuser";
        string apiKey = "8169dabca34b6ae5612a47b63dd97bead3bfXXXX";

        [TestMethod]
        public async Task CreateConfiguration()
        {
            Client c = new Client(userName, apiKey);

            Configuration.Arguments cfArgs = new Configuration.Arguments();
            cfArgs.Add("name", "configuration bindings test");
            cfArgs.Add("configurations", new JObject());
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
