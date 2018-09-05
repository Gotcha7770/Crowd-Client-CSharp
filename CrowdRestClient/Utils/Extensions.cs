using RestSharp;
using RestSharp.Serializers;

namespace CrowdRestClient.Utils
{
    public static class Extensions
    {
        public static IRestRequest WithJsonSerializer(this IRestRequest request, ISerializer serializer)
        {
            request.JsonSerializer = serializer;

            return request;
        }
    }
}
