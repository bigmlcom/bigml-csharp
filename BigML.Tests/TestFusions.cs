using NUnit.Framework;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;
using BigML;

namespace BigML.Tests
{
    /// <summary>
    /// Test resources related with Fusions
    /// </summary>
    [TestFixture()]
    public class TestFusions
    {
        string userName = ConfigurationManager.AppSettings["BIGML_USERNAME"];
        string apiKey = ConfigurationManager.AppSettings["BIGML_API_KEY"];

        [Test()]
        public async Task CreateFusionFromRemoteSource()
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

            // Create Logistic Regression
            LogisticRegression.Arguments argsLR = new LogisticRegression.Arguments();
            argsLR.Add("dataset", ds.Resource);
            LogisticRegression lr = await c.CreateLogisticRegression(argsLR);
            lr = await c.Wait<LogisticRegression>(lr.Resource);

            // Create Tree model
            Model.Arguments argsMd = new Model.Arguments();
            argsMd.Add("dataset", ds.Resource);
            Model md = await c.CreateModel(argsMd);
            md = await c.Wait<Model>(md.Resource);

            // Create Fusion
            Fusion.Arguments argsFs = new Fusion.Arguments();
            List<dynamic> modelIDs = new List<dynamic>();
            modelIDs.Add(lr.Resource);
            modelIDs.Add(md.Resource);
            argsFs.Models = modelIDs;
            Fusion fs = await c.CreateFusion(argsFs);
            fs = await c.Wait<Fusion>(fs.Resource);

            Assert.AreEqual(fs.StatusMessage.StatusCode, Code.Finished);

            // test UPDATE method
            JObject changes = new JObject();
            JArray tags = new JArray();
            tags.Add("Bindings C# test");
            changes.Add("tags", tags);
            fs = await c.Update<Fusion>(fs.Resource, changes);
            Assert.AreEqual(fs.Code, System.Net.HttpStatusCode.Accepted);

            // test DELETE method
            await c.Delete(s);
            await c.Delete(ds);
            await c.Delete(fs);
        }
    }
}