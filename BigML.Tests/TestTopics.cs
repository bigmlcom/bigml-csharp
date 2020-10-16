using NUnit.Framework;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Configuration;
using BigML;

namespace BigML.Tests
{
    /// <summary>
    /// Test resources related with Topic Models and its predictions
    /// </summary>
    [TestFixture()]
    public class TestTopics
    {
        string userName = ConfigurationManager.AppSettings["BIGML_USERNAME"];
        string apiKey = ConfigurationManager.AppSettings["BIGML_API_KEY"];

        [Test()]
        public async Task CreateTopicModelFromRemoteSource()
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

            TopicModel.Arguments argsTM = new TopicModel.Arguments();
            argsTM.Add("dataset", ds.Resource);
            TopicModel tm = await c.CreateTopicModel(argsTM);
            tm = await c.Wait<TopicModel>(tm.Resource);

            // test fails because there is no text field in data
            Assert.AreEqual(tm.StatusMessage.StatusCode, Code.Faulty);

            await c.Delete(s);
            await c.Delete(ds);
            await c.Delete(tm);
        }
    }
}