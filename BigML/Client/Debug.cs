using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace BigML
{
    /// <summary>
    /// BigML client: debug print methods
    /// </summary>
    public partial class Client
    {
        static string lineSeparator = "\n---------------------------";
        JObject _requestContent;

        private void printRequestDebug(string url, JObject request)
        {
            if (this._debug)
            {
                Console.WriteLine(lineSeparator);
                Console.WriteLine(url);
                Console.WriteLine(_requestContent);
            }
        }

        private void printResponseDebug(HttpStatusCode code, JObject response)
        {
            if (this._debug)
            {
                Console.WriteLine("   >>>>>> " + code.ToString() + " >>>>>>   ");
                Console.WriteLine(response);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private void printResponseDebug(HttpStatusCode code, HttpContent response)
        {
            if (this._debug)
            {
                Console.WriteLine("   >>>>>> " + code.ToString() + " >>>>>>   ");
                Console.WriteLine(response);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}