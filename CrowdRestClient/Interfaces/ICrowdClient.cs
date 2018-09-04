using CrowdRestClient.Models;
using RestSharp;
using System;

namespace CrowdRestClient.Interfaces
{
    public interface ICrowdClient
    {
        Uri Uri { get; }

        string UserAgent { get; }

        string AuthenticateSSOUser(AuthenticationContext authenticationContext);

        bool IsTokenValid(string token);

        IRestResponse RestCall(IRestRequest request);
    }
}
