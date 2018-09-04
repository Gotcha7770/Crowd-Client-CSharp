using System.Runtime.Serialization;

namespace CrowdRestClient.Models
{
    /// <summary>
    /// Describes a link response.
    /// </summary>
    [DataContract]
    public class Link
    {
        [DataMember(Name = "href")]
        public string Href { get; set; }

        [DataMember(Name = "rel")]
        public string Rel { get; set; }
    }
}
