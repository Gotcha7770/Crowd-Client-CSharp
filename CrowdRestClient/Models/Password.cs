using System.Runtime.Serialization;

namespace CrowdRestClient.Models
{
    /// <summary>
    /// Describes a Password response.
    /// </summary>
    [DataContract]
    public class Password
    {
        [DataMember(Name = "link")]
        public Link Link { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
