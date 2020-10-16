using NUnit.Framework;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Configuration;
using BigML;

namespace BigML.Tests
{
    /// <summary>
    /// Test resources related with Deepnets
    /// </summary>
    [TestFixture()]
    public class TestDeepnets
    {
        string userName = ConfigurationManager.AppSettings["BIGML_USERNAME"];
        string apiKey = ConfigurationManager.AppSettings["BIGML_API_KEY"];

        [Test()]
        public async Task CreateDeepnetFromRemoteSource()
        {
            // Prepare connection
            Client c = new Client(userName, apiKey);

            // Create source
            Source.Arguments args = new Source.Arguments();
            args.Add("remote", "http://static.bigml.com/csv/iris.csv");
            Source s = await c.CreateSource(args);
            s = await c.Wait<Source>(s.Resource);

            Assert.AreEqual(s.StatusMessage.StatusCode, Code.Finished);

            // Create dataset
            DataSet.Arguments argsDS = new DataSet.Arguments();
            argsDS.Add("source", s.Resource);
            DataSet ds = await c.CreateDataset(argsDS);
            ds = await c.Wait<DataSet>(ds.Resource);

            Assert.AreEqual(ds.StatusMessage.StatusCode, Code.Finished);

            // Create deepnet
            Deepnet.Arguments argsDn = new Deepnet.Arguments();
            argsDn.Add("dataset", ds.Resource);
            Deepnet dn = await c.CreateDeepnet(argsDn);
            dn = await c.Wait<Deepnet>(dn.Resource);

            Assert.AreEqual(dn.StatusMessage.StatusCode, Code.Finished);

            // test UPDATE method
            Newtonsoft.Json.Linq.JObject changes = new Newtonsoft.Json.Linq.JObject();
            Newtonsoft.Json.Linq.JArray tags = new Newtonsoft.Json.Linq.JArray();
            tags.Add("Bindings C# test");
            changes.Add("tags", tags);
            dn = await c.Update<Deepnet>(dn.Resource, changes);
            Assert.AreEqual(dn.Code, System.Net.HttpStatusCode.Accepted);

            // test DELETE method
            await c.Delete(s);
            await c.Delete(ds);
            await c.Delete(dn);
        }
    }
}