using CrowdRestClient.Interfaces;
using CrowdRestClient.Models;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CrowdRestClient.Utils;
using RestSharp.Serializers;

namespace CrowdRestClient
{
    public class CrowdClient : ICrowdClient
    {
        private readonly ISerializer _jsonSerializer = new NewtonsoftJsonSerializer();

        public static readonly string ApiPath = "/rest/usermanagement/{0}/{1}";
        public static readonly string DefaultApiVersion = "latest";

        public IRestClient RestClient { get; }

        public string ApiVersion { get; }

        #region Constructors

        public CrowdClient(string uri, string appName, string appPassword, string apiVersion = null)
            :this(uri, new HttpBasicAuthenticator(appName, appPassword), apiVersion)
        { }

        public CrowdClient(string uri, IAuthenticator authenticator, string apiVersion = null)
        {
            var version = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion;

            ApiVersion = apiVersion ?? DefaultApiVersion;

            RestClient = new RestClient
            {
                BaseUrl = new Uri(uri),
                Authenticator = authenticator,
                UserAgent = $"Crowd.NET /{version} ({Environment.OSVersion}; .NET CLR {Environment.Version})",
                CookieContainer = new CookieContainer()
            };
        }

        public CrowdClient(IRestClient restClient, string apiVersion = null)
        {
            ApiVersion = apiVersion ?? DefaultApiVersion;
            RestClient = restClient;
        }

        #endregion

        private string GetFullUri(string apiResource)
        {
            return Uri + string.Format(ApiPath, ApiVersion, apiResource);
        }

        private IRestRequest CreateRequest(string apiResource)
        {
            var request =  new RestRequest(GetFullUri(apiResource));

            return request;
        }

        #region Interface Implementation

        public Uri Uri => RestClient.BaseUrl;

        public string UserAgent => RestClient.UserAgent;

        public IRestResponse<User> AuthenticateUser(string userName, string password)
        {
            string apiResource = $"authentication.json?username={userName.UrlEncode()}";
            IRestRequest request = new RestRequest(GetFullUri(apiResource))
                .WithJsonSerializer(_jsonSerializer)
                .AddJsonBody(new Password {Value = password});

            return RestClient.Post<User>(request);
        }

        public IRestResponse<string> AuthenticateUser(AuthenticationContext authenticationContext, 
                                                      long duration = 0,
                                                      bool validatePassword = true)
        {
            if (authenticationContext == null)
                throw new ArgumentNullException(nameof(authenticationContext));

            string apiResource = "session?validate-password=";
            apiResource += validatePassword ? "true" : "false";
            apiResource += duration > 0 ? $"duration={duration}" : "";

            IRestRequest request = new RestRequest(GetFullUri(apiResource))
                .WithJsonSerializer(_jsonSerializer)
                .AddJsonBody(authenticationContext);

            IRestResponse<SSOSession> response = RestClient.Post<SSOSession>(request);

            return new CrowdResponse<string>(response, response.Data?.Token);
        }

        public IRestResponse IsTokenValid(string token, List<ValidationFactor> validationFactors = null)
        {
            string apiResource = $"session/{token.UrlEncode()}.json";
            IRestRequest request = new RestRequest(GetFullUri(apiResource));

            return RestClient.Get(request);
        }

        public IRestResponse<SSOSession> GetSession(string token)
        {
            string apiResource = $"session/{token.UrlEncode()}.json";
            IRestRequest request = new RestRequest(GetFullUri(apiResource));

            return RestClient.Get<SSOSession>(request);
        }

        public IRestResponse<User> FindUserFromToken(string token)
        {
            string apiResource = $"session/{token}.json?expand=user";
            IRestRequest request = new RestRequest(GetFullUri(apiResource));

            IRestResponse<SSOSession> response = RestClient.Get<SSOSession>(request);

            return new CrowdResponse<User>(response, response.Data?.User);
        }

        public IRestResponse InvalidateToken(string token)
        {
            string apiResource = $"session/{token.UrlEncode()}.json";
            IRestRequest request = new  RestRequest(GetFullUri(apiResource));

            return RestClient.Delete(request);
        }

        public IRestResponse InvalidateTokensForUser(string username, string exclude = null)
        {
            string apiResource = $"session.json?username={username.UrlEncode()}";
            if (!string.IsNullOrWhiteSpace(exclude))
                apiResource += $"&exclude={exclude.UrlEncode()}";

            IRestRequest request = new RestRequest(GetFullUri(apiResource));

            return RestClient.Delete(request);
        }

        public IRestResponse Execute(IRestRequest request)
        {
            return RestClient.Execute(request);
        }

        public Task<IRestResponse> ExecuteTaskAsync(IRestRequest request, CancellationToken token)
        {
            return RestClient.ExecuteTaskAsync(request, token);
        }

        #endregion
    }
}
