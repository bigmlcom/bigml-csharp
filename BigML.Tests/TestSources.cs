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
    [TestFixture()]
    public class TestSources
    {
        string userName = ConfigurationManager.AppSettings["BIGML_USERNAME"];
        string apiKey = ConfigurationManager.AppSettings["BIGML_API_KEY"];

        [Test()]
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

        [Test()]
        public async Task CreateSourceFromLocal()
        {
            Client c = new Client(userName, apiKey);
            Source s = await c.CreateSource("../../data/SpanishBank.csv", "C# Tests");
            s = await c.Wait<Source>(s.Resource);

            Assert.AreEqual(s.StatusMessage.StatusCode, Code.Finished);

            await c.Delete(s);
        }
    }
}
