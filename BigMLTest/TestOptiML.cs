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
            Source.Parser sp = new Source.Parser();

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

            OptiML.Arguments argsOM = new OptiML.Arguments();
            argsOM.Add("dataset", ds.Resource);
            argsOM.Add("name", "C# tests - TestOptiML");
            OptiML om = await c.CreateOptiML(argsOM);
            om = await c.Wait<OptiML>(om.Resource);

            // This ste can take a bit
            Assert.AreNotEqual(om.StatusMessage.StatusCode, Code.Faulty);

            await c.Delete(s);
            await c.Delete(ds);
            await c.Delete(om);
        }
    }
}
