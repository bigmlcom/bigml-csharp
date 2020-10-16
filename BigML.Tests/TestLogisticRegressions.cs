using NUnit.Framework;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Configuration;
using BigML;

namespace BigML.Tests
{
    /// <summary>
    /// Test resources related with Logistic Regression and its predictions
    /// </summary>
    [TestFixture()]
    public class TestLogisticRegressions
    {
        string userName = ConfigurationManager.AppSettings["BIGML_USERNAME"];
        string apiKey = ConfigurationManager.AppSettings["BIGML_API_KEY"];

        [Test()]
        public async Task CreateLogisticRegressionFromRemoteSource()
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

            LogisticRegression.Arguments argsTM = new LogisticRegression.Arguments();
            argsTM.Add("dataset", ds.Resource);
            LogisticRegression lr = await c.CreateLogisticRegression(argsTM);
            lr = await c.Wait<LogisticRegression>(lr.Resource);

            // test it is finished
            Assert.AreEqual(lr.StatusMessage.StatusCode, Code.Finished);

            // test update method
            Newtonsoft.Json.Linq.JObject changes = new Newtonsoft.Json.Linq.JObject();
            Newtonsoft.Json.Linq.JArray tags = new Newtonsoft.Json.Linq.JArray();
            tags.Add("Bindings C# test");
            changes.Add("tags", tags);
            lr = await c.Update<LogisticRegression>(lr.Resource, changes);
            Assert.AreEqual(lr.Code, System.Net.HttpStatusCode.Accepted);

            await c.Delete(s);
            await c.Delete(ds);
            await c.Delete(lr);
        }
    }
}