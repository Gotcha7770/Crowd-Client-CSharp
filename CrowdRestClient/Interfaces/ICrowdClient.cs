using CrowdRestClient.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CrowdRestClient.Interfaces
{
    public interface ICrowdClient
    {
        Uri Uri { get; }

        string UserAgent { get; }

        string ApiVersion { get; }

        IRestResponse<User> AuthenticateUser(string userName, string password);

        IRestResponse<string> AuthenticateUser(AuthenticationContext authenticationContext, 
                                               long duration = 0, 
                                               bool validatePassword = true);

        IRestResponse IsTokenValid(string token, List<ValidationFactor> validationFactors = null);

        IRestResponse<SSOSession> GetSession(string token);

        IRestResponse<User> FindUserFromToken(string token);

        IRestResponse InvalidateToken(string token);

        IRestResponse InvalidateTokensForUser(string username, string exclude = null); //может ли exclude быть массивом???

        IRestResponse Execute(IRestRequest request);

        Task<IRestResponse> ExecuteTaskAsync(IRestRequest request, CancellationToken token);
    }
}
