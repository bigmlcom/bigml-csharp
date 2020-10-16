using NUnit.Framework;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Configuration;
using BigML;

namespace BigML.Tests
{
    /// <summary>
    /// Test resources related with OptiML
    /// </summary>
    [TestFixture()]
    public class TestOptiML
    {
        string userName = ConfigurationManager.AppSettings["BIGML_USERNAME"];
        string apiKey = ConfigurationManager.AppSettings["BIGML_API_KEY"];

        [Test()]
        public async Task CreateOptiMLFromRemoteSource()
        {
            Client c = new Client(userName, apiKey);

            Project.Arguments pArgs = new Project.Arguments();
            pArgs.Add("name", "Test Project");
            Project p1 = await c.CreateProject(pArgs);
            Project p2 = await c.CreateProject(pArgs);

            Source.Arguments args = new Source.Arguments();
            args.Add("remote", "https://static.bigml.com/csv/iris.csv");
            args.Add("name", "C# tests - Iris");
            // The project can be added as a regular parameter in the creation
            args.Add("project", p1.Resource);

            Source s = await c.CreateSource(args);
            s = await c.Wait<Source>(s.Resource);
            Assert.AreEqual(s.StatusMessage.StatusCode, Code.Finished);

            // Update the project
            Newtonsoft.Json.Linq.JObject changes = new Newtonsoft.Json.Linq.JObject();
            changes.Add("project", p2.Resource);
            s = await c.Update<Source>(s.Resource, changes);
            Assert.AreEqual(s.Code, System.Net.HttpStatusCode.Accepted);


            DataSet.Arguments argsDS = new DataSet.Arguments();
            argsDS.Add("source", s.Resource);
            DataSet ds = await c.CreateDataset(argsDS);
            ds = await c.Wait<DataSet>(ds.Resource);

            Assert.AreEqual(ds.StatusMessage.StatusCode, Code.Finished);

            OptiML.Arguments argsOM = new OptiML.Arguments();
            argsOM.Add("dataset", ds.Resource);
            argsOM.Add("name", "C# tests - TestOptiML");
            argsOM.Add("max_training_time", 100 );
            OptiML om = await c.CreateOptiML(argsOM);
            om = await c.Wait<OptiML>(om.Resource);

            // This step can take a bit
            Assert.AreNotEqual(om.StatusMessage.StatusCode, Code.Faulty);

            await c.Delete(s);
            await c.Delete(ds);
            await c.Delete(om);
            await c.Delete(p1);
            await c.Delete(p2);

        }
    }
}