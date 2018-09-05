using Newtonsoft.Json;
using RestSharp.Serializers;

namespace CrowdRestClient.Utils
{
    public class NewtonsoftJsonSerializer : ISerializer
    {
        public NewtonsoftJsonSerializer()
        {
            ContentType = "application/json";
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string DateFormat { get; set; }

        public string ContentType { get; set; }
    }
}
