using System;
using System.Runtime.Serialization;

namespace CrowdRestClient.Models
{
    [DataContract]
    public class SSOSession
    {
        [DataMember(Name = "expand")]
        public string Expand { get; set; }

        [DataMember(Name = "link")]
        public Link Link { get; set; }

        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "created-date")]
        public long CreatedDateUnixTimestamp { get; set; }

        [DataMember(Name = "expiry-date")]
        public long ExpiryDateUnixTimestamp { get; set; }

        public DateTimeOffset CreatedDate => DateTimeOffset.FromUnixTimeMilliseconds(CreatedDateUnixTimestamp);

        public DateTimeOffset ExpiryDate => DateTimeOffset.FromUnixTimeMilliseconds(ExpiryDateUnixTimestamp);
    }
}
