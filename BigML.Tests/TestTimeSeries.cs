using NUnit.Framework;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Configuration;
using BigML;

namespace BigML.Tests
{
    /// <summary>
    /// Test resources related with Time Series and its predictions
    /// </summary>
    [TestFixture()]
    public class TestTimeSeries
    {
        string userName = ConfigurationManager.AppSettings["BIGML_USERNAME"];
        string apiKey = ConfigurationManager.AppSettings["BIGML_API_KEY"];

        [Test()]
        public async Task CreateTimeSeriesFromRemoteSource()
        {
            Client c = new Client(userName, apiKey);
            Source.Arguments args = new Source.Arguments();
            args.Add("remote", "https://static.bigml.com/csv/iris.csv");
            args.Add("name", "C# tests - Iris");

            Source s = await c.CreateSource(args);
            s = await c.Wait<Source>(s.Resource);

            Assert.AreEqual(s.StatusMessage.StatusCode, Code.Finished);

            DataSet.Arguments argsDS = new DataSet.Arguments();
            argsDS.Add("source", s.Resource);
            DataSet ds = await c.CreateDataset(argsDS);
            ds = await c.Wait<DataSet>(ds.Resource);

            Assert.AreEqual(ds.StatusMessage.StatusCode, Code.Finished);

            TimeSeries.Arguments argsTM = new TimeSeries.Arguments();
            argsTM.Add("dataset", ds.Resource);
            TimeSeries ts = await c.CreateTimeSeries(argsTM);
            ts = await c.Wait<TimeSeries>(ts.Resource);

            // test it is finished
            Assert.AreEqual(ts.StatusMessage.StatusCode, Code.Finished);

            // test update method
            Newtonsoft.Json.Linq.JObject changes = new Newtonsoft.Json.Linq.JObject();
            Newtonsoft.Json.Linq.JArray tags = new Newtonsoft.Json.Linq.JArray();
            tags.Add("Bindings C# test");
            changes.Add("tags", tags);
            ts = await c.Update<TimeSeries>(ts.Resource, changes);
            Assert.AreEqual(ts.Code, System.Net.HttpStatusCode.Accepted);

            await c.Delete(s);
            await c.Delete(ds);
            await c.Delete(ts);
        }
    }
}