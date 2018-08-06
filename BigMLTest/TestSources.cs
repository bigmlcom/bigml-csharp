using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using BigML;

namespace BigMLTest
{
   /// <summary>
   /// 
   /// </summary>
    [TestClass]
    public class TestSources
    {
        string userName = "myuser";
        string apiKey = "8169dabca34b6ae5612a47b63dd97bead3bfXXXX";

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
        public async Task CreateSourceFromLocal()
        {
            Client c = new Client(userName, apiKey);
            Source s = await c.CreateSource("C:/Users/You/Downloads/train.tsv", "C# Tests");
            s = await c.Wait<Source>(s.Resource);
        
            Assert.AreEqual(s.StatusMessage.StatusCode, Code.Finished);
        
            await c.Delete(s);
        }
    }
}
