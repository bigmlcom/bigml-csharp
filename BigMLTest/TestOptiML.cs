using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using BigML;

namespace BigMLTest
{
    /// <summary>
    /// Test resources related with OptiML
    /// </summary>
    [TestClass]
    public class TestOptiML
    {
        string userName = "myUser";
        string apiKey = "8169dabca34b6ae5612a47b63dd97bead3bfeXXX";

        [TestMethod]
        public async Task CreateOptiMLFromRemoteSource()
        {
            Client c = new Client(userName, apiKey);
            Source.Arguments args = new Source.Arguments();

            args.Add("remote", "https://static.bigml.com/csv/iris.csv");
            args.Add("name", "C# tests - Iris");
            // The project can be added as a regular parameter in the creation
            args.Add("project", "project/58a7147e663ac2321f00239f");

            Source s = await c.CreateSource(args);
            s = await c.Wait<Source>(s.Resource);
            Assert.AreEqual(s.StatusMessage.StatusCode, Code.Finished);

            // Update the project
            Newtonsoft.Json.Linq.JObject changes = new Newtonsoft.Json.Linq.JObject();
            changes.Add("project", "project/5878b1cb7e0a8d5cc500de61");
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
            OptiML om = await c.CreateOptiML(argsOM);
            om = await c.Wait<OptiML>(om.Resource);

            // This step can take a bit
            Assert.AreNotEqual(om.StatusMessage.StatusCode, Code.Faulty);

            await c.Delete(s);
            await c.Delete(ds);
            await c.Delete(om);
        }
    }
}
