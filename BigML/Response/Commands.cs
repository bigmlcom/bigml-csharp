using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BigML
{
    public partial class Client
    {

        const string BigML = "{3}://{4}/{2}andromeda/{5}?username={0};api_key={1};{6}";
        const string BigMLOp = "{3}://{4}/{2}andromeda/{5}/{6}?username={0};api_key={1}";
        const string BigMLList = "{3}://{4}/{2}andromeda/{5}?{6};username={0};api_key={1}";

        /// <summary>
        /// Map type name to resource type name (Source --> source, ...)
        /// </summary>
        static string ResourceTypeName<T>() where T : Response
        {
            return typeof(T).Name.ToLower();
        }


        /// <summary>
        /// Create a new resource using supplied arguments
        /// </summary>
        public Task<T> Create<T>(Response.Arguments<T> arguments) where T : Response, new()
        {
            this._requestContent = arguments.ToJson();
            var content = new JsonContent(this._requestContent);
            
            return Create<T>(content);
        }

        /// <summary>
        /// Helper to create resource given HttpContent.
        /// </summary>
        private async Task<T> Create<T>(HttpContent request) where T : Response, new()
        {
            HttpClient client = new HttpClient();
            if (ResourceTypeName<T>() == "source")
            {
                client.Timeout = TimeSpan.FromHours(1);
            }

            var url = string.Format(BigML, _username, _apiKey, _dev, _protocol, _VpcDomain, ResourceTypeName<T>(), "");
            //printRequestDebug(url, this._requestContent);
            var response = await client.PostAsync(url, request).ConfigureAwait(_useContextInAwaits);
            var resourceStr = await response.Content.ReadAsStringAsync().ConfigureAwait(_useContextInAwaits);
            var resource = JsonConvert.DeserializeObject(resourceStr);
            //printResponseDebug(response.StatusCode, resource);

            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                case HttpStatusCode.Accepted:
                    return new T { Object = resource, Location = response.Headers.Location };

                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.PaymentRequired:
                case HttpStatusCode.NotFound:
                    return new T { Object = resource };

                default:
                    return new T();
            }
        }


        /// <summary>
        /// Delete a resource.
        /// </summary>
        public Task<Response> Delete<T>(T resource, string query = "") where T : Response
        {
            return Delete(resource.Resource, query);
        }

        /// <summary>
        /// Delete a resource.
        /// </summary>
        public async Task<Response> Delete(string resourceId, String query = "")
        {
            if (resourceId == null)
                throw new ArgumentNullException("resourceId");

            var client = new HttpClient();
            var url = string.Format(BigML, _username, _apiKey, _dev, _protocol, _VpcDomain, resourceId, query);
            printRequestDebug(url, null);
            var response = await client.DeleteAsync(url).ConfigureAwait(_useContextInAwaits);
            printResponseDebug(response.StatusCode, response.Content);
            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent:
                    return new Response { Object = new JObject {{ "code", (int)response.StatusCode } } };

                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.PaymentRequired:
                case HttpStatusCode.NotFound:
                    return new Response { Object = JValue.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(_useContextInAwaits)) };

                default:
                    return new Response();
            }
        }


        /// <summary>
        /// Retrieve a resource.
        /// </summary>
        public Task<T> Get<T>(T resource) where T : Response, new()
        {
            return Get<T>(resource.Resource);
        }

        /// <summary>
        /// Retrieve a resource.
        /// </summary>
        public async Task<T> Get<T>(string resourceId) where T : Response, new()
        {
            if (resourceId == null)
                throw new ArgumentNullException("resourceId");

            var client = new HttpClient();
            var url = string.Format(BigML, _username, _apiKey, _dev, _protocol, _VpcDomain, resourceId, "");
            //printRequestDebug(url, null);
            var response = await client.GetAsync(url).ConfigureAwait(_useContextInAwaits);
            var resourceStr = await response.Content.ReadAsStringAsync().ConfigureAwait(_useContextInAwaits);
            var resource = JsonConvert.DeserializeObject(resourceStr);
            //printResponseDebug(response.StatusCode, resource);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Accepted:
                    return new T { Object = resource };

                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.PaymentRequired:
                case HttpStatusCode.NotFound:
                    return new T { Object = resource };

                default:
                    return new T();
            }
        }


        /// <summary>
        /// Check whether a resource's status is FINISHED.
        /// </summary>
        public Task<bool> IsReady<T>(T resource) where T : Response, new()
        {
            return IsReady(resource.Resource);
        }

        /// <summary>
        /// Check whether a resource's status is FINISHED.
        /// </summary>
        public async Task<bool> IsReady(string resourceId)
        {
            var response = await Get<Response>(resourceId).ConfigureAwait(_useContextInAwaits);
            return response.Code == HttpStatusCode.OK
                    && (Code)(int)response.Object.status.code == Code.Finished;
        }


        /// <summary>
        /// Check element status periodically up to finish.
        /// </summary>
        public async Task<T> Wait<T>(string resourceId) where T : Response, new()
        {
            if (resourceId == null)
                throw new ArgumentNullException("resourceId");

            T resource = await this.Get<T>(resourceId).ConfigureAwait(_useContextInAwaits);
            Code resourceStatusCode = (Code)(int)resource.Object.status.code;
            while (resourceStatusCode != Code.Finished && resourceStatusCode != Code.Faulty)
            {
                await Task.Delay(1000).ConfigureAwait(_useContextInAwaits);
                resource = await this.Get<T>(resourceId).ConfigureAwait(_useContextInAwaits);
                resourceStatusCode = (Code)(int)resource.Object.status.code;
            }
            return resource;
        }


        /// <summary>
        /// List all resources
        /// </summary>
        public async Task<Listing<T>> List<T>(string query) where T : Response, new()
        {
            var client = new HttpClient();
            var url = string.Format(BigMLList, _username, _apiKey, _dev, _protocol, _VpcDomain, ResourceTypeName<T>(), query);
            //printRequestDebug(url, null);
            var response = await client.GetAsync(url).ConfigureAwait(_useContextInAwaits);
            var resourceStr = await response.Content.ReadAsStringAsync().ConfigureAwait(_useContextInAwaits);
            var resource = JsonConvert.DeserializeObject(resourceStr);
            //printResponseDebug(response.StatusCode, resource);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new Listing<T> { Object = resource };

                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.PaymentRequired:
                case HttpStatusCode.NotFound:
                    return new Listing<T> { Object = resource };

                default:
                    return new Listing<T>();
            }
        }


        /// <summary>
        /// Extra operation on a resource.
        /// </summary>
        public async Task<T> Operation<T>(string resourceId, string operation_name) where T: Response, new()
        {
            if (resourceId == null)
                throw new ArgumentNullException("resourceId");

            var client = new HttpClient();
            var url = string.Format(BigMLOp, _username, _apiKey, _dev, _protocol, _VpcDomain, resourceId, operation_name);
            //printRequestDebug(url, null);

            var response = await client.GetAsync(url).ConfigureAwait(_useContextInAwaits);
            var resourceStr = await response.Content.ReadAsStringAsync().ConfigureAwait(_useContextInAwaits);
            var resource = JsonConvert.DeserializeObject(resourceStr);
            //printResponseDebug(response.StatusCode, resource);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Accepted:
                    return new T { Object = resource };

                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.PaymentRequired:
                case HttpStatusCode.NotFound:
                    return new T { Object = resource };

                default:
                    return new T();
            }
        }


        /// <summary>
        /// Download resource content, no its JSON
        /// Available operation for datasets
        /// If dataset export is not available, request for it.
        /// </summary>
        private async Task<bool> Download(string resourceId, FileStream file)
        {
            if (resourceId == null)
                throw new ArgumentNullException("resourceId");

            var client = new HttpClient();
            var url = string.Format(BigMLOp, _username, _apiKey, _dev, _protocol, _VpcDomain, resourceId, "download");
            //printRequestDebug(url, null);

            var response = await client.GetAsync(url).ConfigureAwait(_useContextInAwaits);
            await response.Content.CopyToAsync(file).ConfigureAwait(_useContextInAwaits);
            //printResponseDebug(response.StatusCode, response.Content);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Accepted:
                    return true;

                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.PaymentRequired:
                case HttpStatusCode.NotFound:
                    return false;

                default:
                    return false;
            }
        }


        /// <summary>
        /// Update a resource.
        /// </summary>
        public Task<T> Update<T>(T resource, string name) where T : Response, new()
        {
            return Update<T>(resource.Resource, name);
        }

        /// <summary>
        /// Update a resource with Json payload
        /// </summary>
        public async Task<T> Update<T>(string resourceId, JObject changes) where T : Response, new()
        {
            if (resourceId == null)
                throw new ArgumentNullException("resourceId");

            var client = new HttpClient();
            var content = new JsonContent(changes);

            var url = string.Format(BigML, _username, _apiKey, _dev, _protocol, _VpcDomain, resourceId, "");
            //printRequestDebug(url, changes);

            var response = await client.PutAsync(url, content).ConfigureAwait(_useContextInAwaits);
            var resourceStr = await response.Content.ReadAsStringAsync().ConfigureAwait(_useContextInAwaits);
            var resource = JsonConvert.DeserializeObject(resourceStr);
            //printResponseDebug(response.StatusCode, resource);

            switch (response.StatusCode)
            {
                // HttpStatusCode.OK is never returned in Update op.
                case HttpStatusCode.Accepted:
                    return new T { Object = resource };

                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.PaymentRequired:
                case HttpStatusCode.NotFound:
                    return new T { Object = resource };

                default:
                    return new T();
            }
        }

        /// <summary>
        /// Update a resource's name.
        /// </summary>
        public Task<T> Update<T>(string resourceId, string name) where T : Response, new()
        {
            JObject changes = new JObject();
            if (!string.IsNullOrWhiteSpace(name)) changes["name"] = name;
            return Update<T>(resourceId, changes);
        }

    }
}
