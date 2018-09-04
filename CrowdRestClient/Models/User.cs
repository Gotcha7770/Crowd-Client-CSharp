using System.Runtime.Serialization;

namespace CrowdRestClient.Models
{
    /// <summary>
    /// Describes a User response.
    /// </summary>
    [DataContract(Name = "user", Namespace = "")]
    public class User
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "first-name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last-name")]
        public string LastName { get; set; }

        [DataMember(Name = "display-name")]
        public string DisplayName { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "active")]
        public bool IsActive { get; set; }

        [DataMember(Name = "expand")]
        public string Expand { get; set; }

        [DataMember(Name = "link")]
        public Link Link { get; set; }

        [DataMember(Name = "password")]
        public Password Password { get; set; }
    }
}
