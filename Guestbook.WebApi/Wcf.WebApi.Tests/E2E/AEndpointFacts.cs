using GuestBook.WebApi.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using System;
using System.Globalization;
using System.Net;

namespace GuestBook.WebApi.Tests.E2E
{
    public abstract class AEndpointFacts
    {
        protected string _baseUrl = "http://localhost:5501";
        protected string _token;
        protected string _clientId = "ED178BF266B749E5A7173C0AE6113C67";
        protected string _clientSecret = "ThEceb38RAdupHAwaqUrUruqEqa6aceSweWe24EtreyUj3v8da";
        protected string _grantType = "password";
        protected string _scope = "admin_area";

        public AEndpointFacts()
        {
            _token = GetToken(
                username: SystemUser.UserName,
                password: SystemUser.Password
            );
        }

        public TModel Create<TEditModel, TModel>(TEditModel model, string url)
            where TEditModel : new()
            where TModel : new()
        {
            var request = CreateRequest(model, url, null);
            var client = CreateClient();
            var res = client.Post<TModel>(request);
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return res.Data;// JsonConvert.DeserializeObject<TModel>(res.Content);
            }
            else
            {
                var content = res.Content.Replace(@"\r\n", "\r\n");
                throw new Exception($"Wrong status code: {res.StatusCode}. Content: {content}");
            }
        }

        public TModel Get<TModel>(string url, params string[][] parameters)
            where TModel : new()
        {
            var request = CreateRequest<object>(null, url, parameters);
            var client = CreateClient();
            var res = client.Get<TModel>(request);
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return res.Data;
            }
            else if (res.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception($"Wrong status code: {res.StatusCode}. {res.Content}");
            }
            else
            {
                throw new Exception($"Wrong status code: {res.StatusCode}. {res.Content}");
            }
        }

        public ItemList<TModel> List<TModel>(string url, params string[][] parameters)
            where TModel : new()
        {
            var request = CreateRequest<Object>(null, url, parameters);
            var client = CreateClient();
            var res = client.Get<ItemList<TModel>>(request);
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return res.Data;
            }
            else if (res.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new Exception();
            }
            else
            {
                throw new Exception($"Wrong status code: {res.StatusCode}. {res.Content}");
            }
        }

        public TModel Update<TEditModel, TModel>(TEditModel model, string url)
            where TEditModel : new()
            where TModel : new()
        {
            var request = CreateRequest(model, url, null);
            var client = CreateClient();
            var res = client.Put<TModel>(request);
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return res.Data;
            }
            else
            {
                throw new Exception($"Wrong status code: {res.StatusCode}. {res.Content}");
            }
        }

        public RemovedItemContract Delete(string url)
        {
            var request = new RestRequest(url);
            var client = CreateClient();
            var res = client.Delete<RemovedItemContract>(request);
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return res.Data;
            }
            else
            {
                throw new Exception($"Wrong status code: {res.StatusCode}. {res.Content}");
            }
        }

        protected static RestRequest CreateRequest<TEditModel>(TEditModel model, string url, string[][] parameters)
            where TEditModel : new()
        {
            var request = new RestRequest(url);
            request.JsonSerializer = new JsonSerializer();
            if (model != null)
            {
                request.AddJsonBody(model);
            }

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    request.AddQueryParameter(parameter[0], parameter[1]);
                }
            }
            return request;
        }

        protected virtual RestClient CreateClient()
        {
            var client = new RestClient(_baseUrl);
            client.AddDefaultHeader("Content-Type", "application/json");
            client.AddDefaultHeader("Authorization", $"Bearer {_token}");
            client.AddHandler("application/json", new JsonDeserializer());
            return client;
        }

        protected string GetToken(string clientId, string clientSecred, string grantType, string scope,
            string username = null, string password = null)
        {
            var client = new RestClient(_baseUrl);

            var request = new RestRequest("connect/token");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecred);
            request.AddParameter("grant_type", grantType);
            request.AddParameter("scope", scope);
            if (grantType == "password")
            {
                request.AddParameter("username", username);
                request.AddParameter("password", password);
            }

            var res = client.Post<Token>(request);
            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Wrong status code: {res.StatusCode}. {res.Content}");
            }

            return res.Data.access_token;
        }

        protected string GetToken(string username = null, string password = null)
        {
            return GetToken(_clientId, _clientSecret, _grantType, _scope, username, password);
        }
        public class JsonSerializer : ISerializer
        {
            /// <summary>Unused for JSON Serialization</summary>
            public string DateFormat { get; set; }

            /// <summary>Unused for JSON Serialization</summary>
            public string RootElement { get; set; }

            /// <summary>Unused for JSON Serialization</summary>
            public string Namespace { get; set; }

            /// <summary>Content type for serialized content</summary>
            public string ContentType { get; set; }

            /// <summary>Default serializer</summary>
            public JsonSerializer()
            {
                this.ContentType = "application/json";
            }

            /// <summary>Serialize the object as JSON</summary>
            /// <param name="obj">Object to serialize</param>
            /// <returns>JSON as String</returns>
            public string Serialize(object obj)
            {
                return JsonConvert.SerializeObject(obj);
            }
        }

        public class JsonDeserializer : IDeserializer
        {
            public string RootElement { get; set; }

            public string Namespace { get; set; }

            public string DateFormat { get; set; }

            public CultureInfo Culture { get; set; }

            public JsonDeserializer()
            {
                this.Culture = CultureInfo.InvariantCulture;
            }

            public T Deserialize<T>(IRestResponse response)
            {
                return JsonConvert.DeserializeObject<T>(response.Content, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            }
        }

        protected void DeleteSafe(string url)
        {
            try
            {
                Delete(url);
            }
            catch
            {
            }
        }
    }
}
