using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Net.Http.Headers;
using System.Text;

namespace System.Net.Http
{
    /// <summary>
    /// Helper class for Json content
    /// </summary>
    public class JsonContent : StringContent
    {
        public JsonContent(JsonValue content): base(content.ToString(), Encoding.UTF8, "application/json")
        {
        }
    }

    /// <summary>
    /// Helpers for addding content to MultiFormDataContent
    /// </summary>
    public static class Helpers
    {
        public static void Add(this MultipartFormDataContent source, KeyValuePair<string, JsonValue> contentName)
        {
            source.Add(contentName.Value, contentName.Key);
        }

        public static void Add(this MultipartFormDataContent source, JsonObject json)
        {
            foreach(var kv in json)
            {
                source.Add(kv);
            };
        }

        public static void Add(this MultipartFormDataContent source, string content, string name)
        {
            source.Add(new StringContent(content, Encoding.UTF8), name);
        }

        public static void Add(this MultipartFormDataContent source, JsonValue content, string name)
        {
            source.Add(new JsonContent(content), name);
        }

        public static void Add(this MultipartFormDataContent source, int content, string name)
        {
            source.Add(new StringContent(content.ToString(), Encoding.UTF8), name);
        }

        public static void Add(this MultipartFormDataContent source, bool content, string name)
        {
            source.Add(new StringContent(content.ToString(), Encoding.UTF8), name);
        }

        public static void Add(this MultipartFormDataContent source, Stream content, MediaTypeHeaderValue  mediaType, string fileName)
        {
            var part = new StreamContent(content);
            part.Headers.ContentType = mediaType;
            source.Add(part, @"""file""", string.Format(@"""{0}""", fileName));
        }

        public static void Add(this MultipartFormDataContent source, Stream content, string fileName)
        {
            source.Add(content, new MediaTypeHeaderValue("text/csv"), fileName);
        }
    }
}