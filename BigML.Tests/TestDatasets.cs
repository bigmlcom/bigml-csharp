using NUnit.Framework;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Configuration;
using BigML;


namespace BigML.Tests
{
    /// <summary>
    /// 
    /// </summary>
    ///     
    [TestFixture()]
    public class TestDatasets
    {
        string userName = ConfigurationManager.AppSettings["BIGML_USERNAME"];
        string apiKey = ConfigurationManager.AppSettings["BIGML_API_KEY"];

        [Test()]
        public async Task CreateDataset()
        {
            Client c = new Client(userName, apiKey);
            Source.Arguments args = new Source.Arguments();
            args.Add("remote", "https://static.bigml.com/csv/iris.csv");

            Source s = await c.CreateSource(args);
            s = await c.Wait<Source>(s.Resource);

            Assert.AreEqual(s.StatusMessage.StatusCode, Code.Finished);

            DataSet.Arguments argsDS = new DataSet.Arguments();
            argsDS.Source = s.Resource;
            DataSet ds = await c.CreateDataset(argsDS);
            ds = await c.Wait<DataSet>(ds.Resource);

            await c.Delete(s);
            await c.Delete(ds);
        }

        [Test()]
        public async Task CreateMultiDataset()
        {
            Client c = new Client(userName, apiKey);
            Source.Arguments args = new Source.Arguments();
            args.Add("remote", "https://static.bigml.com/csv/iris.csv");
            args.Add("name", "https://static.bigml.com/csv/iris.csv");

            Source s = await c.CreateSource(args);
            s = await c.Wait<Source>(s.Resource);

            Assert.AreEqual(s.StatusMessage.StatusCode, Code.Finished);

            DataSet.Arguments argsDS = new DataSet.Arguments();
            argsDS.Source = s.Resource;
            DataSet ds = await c.CreateDataset(argsDS);
            ds = await c.Wait<DataSet>(ds.Resource);

            // use the other DataSet constructor
            argsDS = new DataSet.Arguments(s);
            DataSet ds2 = await c.CreateDataset(argsDS);
            ds2 = await c.Wait<DataSet>(ds2.Resource);

            argsDS = new DataSet.Arguments();
            argsDS.OriginDatasets.Add(ds.Resource);
            argsDS.OriginDatasets.Add(ds2.Resource);
            argsDS.Name = "Dataset using multi datasets";

            DataSet dsMulti = await c.CreateDataset(argsDS);
            dsMulti = await c.Wait<DataSet>(dsMulti.Resource);

            await c.Delete(s);
            await c.Delete(ds);
            await c.Delete(ds2);
            await c.Delete(dsMulti);
        }
    }
}