using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using BigML;

namespace BigMLTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class BigMLTests
    {
        string userName = "user";
        string apiKey = "1a07911f835dede4607cfafa3aa4b60d9bdXXXXX";

        [TestMethod]
        public async Task CreateSourceFromRemote()
        {
            Client c = new Client(userName, apiKey);
            Source.Arguments args = new Source.Arguments();
            args.Add("remote", "https://static.bigml.com/csv/iris.csv");

            Source s = await c.CreateSource(args);
            s = await c.Wait<Source>(s.Resource);

            Assert.AreEqual(s.StatusMessage.StatusCode, Code.Finished);

            await c.Delete(s);
        }

        [TestMethod]
        public async Task CreateDataset()
        {
            Client c = new Client(userName, apiKey);
            Source.Arguments args = new Source.Arguments();
            args.Add("remote", "https://static.bigml.com/csv/iris.csv");

            Source s = await c.CreateSource(args);
            s = await c.Wait<Source>(s.Resource);

            Assert.AreEqual(s.StatusMessage.StatusCode, Code.Finished);

            DataSet.Arguments argsDS = new DataSet.Arguments();
            DataSet ds = await c.CreateDataset(argsDS);
            ds = await c.Wait<DataSet>(ds.Resource);

            await c.Delete(s);
            await c.Delete(ds);
        }
    }
}
