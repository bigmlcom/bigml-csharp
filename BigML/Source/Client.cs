using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create new Source from local file
        /// </summary>
        /// <param name="path">path to local file</param>
        /// <param name="fileName">name to use</param>
        /// <param name="arguments">Additional parameters</param>
        [System.Obsolete("Create is deprecated, use CreateSource instead.")]
        public Task<Source> Create(string path, string fileName = null, Source.Arguments arguments = null) 
        {
            return CreateSource(path, fileName, arguments);
        }

        /// <summary>
        /// Create new Source from local file
        /// </summary>
        /// <param name="path">path to local file</param>
        /// <param name="fileName">name to use</param>
        /// <param name="arguments">Additional parameters</param>
        public Task<Source> CreateSource(string path, string fileName = null, Source.Arguments arguments = null)
        {
            fileName = fileName ?? Path.GetFileName(path);

            var boundary = string.Format("--{0}", Guid.NewGuid());

            var request = new MultipartFormDataContent(boundary);
            var customContentType = new MediaTypeHeaderValue("multipart/form-data");
            customContentType.Parameters.Add(new NameValueHeaderValue("boundary", boundary));
            request.Headers.ContentType = customContentType;

            request.Add(File.OpenRead(path), fileName);
            if (arguments != null) request.Add(arguments.ToJson() as JsonObject);

            return Create<Source>(request);
        }

        /// <summary>
        /// Create a new Source
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        [System.Obsolete("Create is deprecated, use CreateSource instead.")]
        public Task<Source> Create(Source.Arguments arguments)
        {
            return CreateSource(arguments);
        }

        /// <summary>
        /// Create a new Source
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public Task<Source> CreateSource(Source.Arguments arguments)
        {
            return arguments.File != null 
                ? CreateSource(arguments.File, arguments.Name, arguments) 
                : Create<Source>(arguments);
        }

        /// <summary>
        /// Create a new source from provided data and optional header.
        /// </summary>
        [System.Obsolete("Create is deprecated, use CreateSource instead.")]
        public Task<Source> Create(IEnumerable<string> data, string name = null, string header = null)
        {
            return CreateSource(data, name, header);
        }

        /// <summary>
        /// Create a new source from provided data and optional header.
        /// </summary>
        public Task<Source> CreateSource(IEnumerable<string> data, string name = null, string header = null)
        {
            return Create<Source>(new Source.Arguments
            {
                Name = name,
                Data = Enumerable.Repeat(header, Convert.ToInt32(!string.IsNullOrWhiteSpace(header))).Concat(data)
            });
        }

        /// <summary>
        /// Update a source.
        /// </summary>
        /// <param name="source">A valid source</param>
        /// <param name="name">The new name of the source</param>
        /// <param name="parser">New parse options for the source</param>
        public Task<Source> Update(Source source, string name = null, Source.Parser parser = null)
        {
            return Update(source.Resource, name, parser);
        }

        /// <summary>
        /// Update a source.
        /// </summary>
        /// <param name="resourceId">A valid source/id</param>
        /// <param name="name">The new name of the source</param>
        /// <param name="parser">New parse options for the source</param>
        public Task<Source> Update(string resourceId, string name = null, Source.Parser parser = null)
        {
            dynamic json = new JsonObject();

            if(!string.IsNullOrWhiteSpace(name)) json.name = name;
            if(parser != null) json.source_parser = parser.ToJson();

            return Update(resourceId, json);
        }

        public Query<Source.Filterable, Source.Orderable, Source> ListSources()
        {
            return new SourceListing(List<Source>);
        }
    }
}