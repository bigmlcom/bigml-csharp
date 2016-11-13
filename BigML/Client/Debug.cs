using System;
using System.Net;
using System.Net.Http;
using System.Json;

namespace BigML
{
    /// <summary>
    /// BigML client.
    /// </summary>
    public partial class Client
    {
        static string lineSeparator = "\n---------------------------";
        JsonValue _requestContent;

        private void printRequestDebug(string url, JsonValue request)
        {
            if (this._debug)
            {
                Console.WriteLine(lineSeparator);
                Console.WriteLine(url);
                Console.WriteLine(_requestContent);
            }
        }

        private void printResponseDebug(HttpStatusCode code, JsonValue response)
        {
            if (this._debug)
            {
                Console.WriteLine("   >>>>>> " + code.ToString() + " >>>>>>   ");
                Console.WriteLine(response);
                Console.WriteLine("\n\n");
            }
        }

        private void printResponseDebug(HttpStatusCode code, HttpContent response)
        {
            if (this._debug)
            {
                Console.WriteLine("   >>>>>> " + code.ToString() + " >>>>>>   ");
                Console.WriteLine(response);
                Console.WriteLine("\n\n");
            }
        }
    }
}