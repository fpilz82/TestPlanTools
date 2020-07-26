using System;
using TestPlanTools.Extensions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Serilog;

namespace TestPlanTools.Services
{
    public class GenericRestClient
    {
        private DevOpsConfiguration _config;
        private string _token;

        public GenericRestClient(DevOpsConfiguration config)
        {
            _config = config;
        }

        public IRestResponse Get(string path) =>
            SendRequest<dynamic>(path, Method.GET, null);

        public IRestResponse Post<T>(string path, T body) where T : class =>
            SendRequest<T>(path, Method.POST, body);

        public IRestResponse Patch(string path, string body) =>
            SendRequest<dynamic>(path, Method.PATCH, body);

        private IRestResponse SendRequest<T>(string path, Method method, T body)
        {
            var url = _config.Url.UrlAppend(_config.Project).UrlAppend("/_apis").UrlAppend(path);
            Log.Logger.Information("Sending a {0} request to {1}", method, url);
            var client = new RestClient(url)
            {
                Authenticator = new HttpBasicAuthenticator(_config.User, _config.Token.ToString())
            };

            var request = new RestRequest
            {
                Method = method,
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json-patch+json");

            if (body != null)
            {
                Log.Logger.Information("Body = {0}", body);
                request.AddJsonBody(body);
            }

            var response = client.Execute(request);

            Log.Logger.Information("Response status = {0} {1}", response.StatusCode, response.StatusDescription);
            Log.Logger.Information("Response has body = {0}", response.Content == null);
            return response;
        }

        private protected T ParseResponse<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content.ToJson()["value"].ToString());
        }
    }
}