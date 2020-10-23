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
    public class TestExecutions
    {
        string userName = ConfigurationManager.AppSettings["BIGML_USERNAME"];
        string apiKey = ConfigurationManager.AppSettings["BIGML_API_KEY"];

        [Test()]
        public async Task CreateExecution()
        {
            Client c = new Client(userName, apiKey);
            Script.Arguments args = new Script.Arguments();
            // The script will add two numbers, a + b, so the inputs argument
            // is an array with two elements describing those inputs
            JArray scriptInputs = new JArray();
            JObject input = new JObject();
            // The first input variable name is "a" and is expected to be a number
            input.Add("name", "a");
            input.Add("type", "numeric");
            input.Add("default", 0);
            scriptInputs.Add(input);
            // the second input variable name is "a" and is expected to be a number
            input = new JObject();
            input.Add("name", "b");
            input.Add("type", "numeric");
            input.Add("default", 0);
            scriptInputs.Add(input);
            // WhizzML code to add the numbers
            String sourceCode = "(+ a b)";

            args.Add("name", "Test script to add two numbers.");
            args.Add("source_code", sourceCode);
            args.Add("inputs", scriptInputs);


            Script s = await c.CreateScript(args);
            s = await c.Wait<Script>(s.Resource);
            Assert.AreEqual(s.StatusMessage.StatusCode, Code.Finished);

            // The script is ready to be run. Now, let's execute it

            Execution.Arguments eArgs = new Execution.Arguments();
            // Values are assigned to the inputs of the script
            JArray executionInputs = new JArray();
            JArray eInput = new JArray();
            // a=3
            eInput.Add("a");
            eInput.Add(3);
            executionInputs.Add(eInput);
            // b=2
            eInput = new JArray();
            eInput.Add("b");
            eInput.Add(2);
            executionInputs.Add(eInput);

            eArgs.Add("name", "Test Execution: adding 2+3");
            eArgs.Add("script", s.Resource);
            eArgs.Add("inputs", executionInputs);

            // And run the execution for these values
            Execution e = await c.CreateExecution(eArgs);
            e = await c.Wait<Execution>(e.Resource);
            Assert.AreEqual(e.StatusMessage.StatusCode, Code.Finished);

            // check the result
            Assert.AreEqual((int) e.Result, 5);

            await c.Delete(s);
            await c.Delete(e);
        }

    }
}
