using CrowdRestClient.Interfaces;
using CrowdRestClient.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Extensions;
using System;
using System.Net;
using System.Reflection;

namespace CrowdRestClient
{
    public class CrowdClientImpl : ICrowdClient
    {
        public static readonly string ApiPath = "/rest/usermanagement/{0}/{1}";
        public static readonly string ApiVersion = "latest";

        public IRestClient RestClient { get; }

        #region Constructors

        public CrowdClientImpl(string uri, string appName, string appPassword)
        {
            var version = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion;

            RestClient = new RestClient
            {
                BaseUrl = new Uri(uri),
                Authenticator = new HttpBasicAuthenticator(appName, appPassword),
                UserAgent = $"Crowd.NET /{version} ({Environment.OSVersion}; .NET CLR {Environment.Version})",
                CookieContainer = new CookieContainer()
            };
        }

        public CrowdClientImpl(IRestClient restClient)
        {
            RestClient = restClient;
        }

        #endregion

        #region Interface Implementation

        public Uri Uri => RestClient.BaseUrl;

        public string UserAgent => RestClient.UserAgent;

        public string AuthenticateSSOUser(AuthenticationContext authenticationContext)
        {
            string apiResource = Uri + string.Format(ApiPath, ApiVersion, "session?validate-password=true");
            IRestRequest request = new RestRequest(apiResource);
            string body = JsonConvert.SerializeObject(authenticationContext);
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            IRestResponse response = RestClient.Post(request);

            HttpStatusCode status = response.StatusCode;
            if (status != HttpStatusCode.Created)
            {
                return string.Empty;
            }

            var session = JsonConvert.DeserializeObject<SSOSession>(response.Content);

            return session.Token;
        }

        public bool IsTokenValid(string token)
        {
            string apiResource = Uri + string.Format(ApiPath, ApiVersion, $"session/{token.UrlEncode()}.json");
            IRestRequest request = new RestRequest(apiResource);
            IRestResponse response = RestClient.Get(request);

            return response.IsSuccessful;
        }

        public IRestResponse RestCall(IRestRequest request)
        {
            return RestClient.Execute(request);
        }

        #endregion
    }
}
