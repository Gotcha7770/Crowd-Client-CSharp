using System.Runtime.Serialization;

namespace CrowdRestClient.Models
{
    [DataContract(Name = "authentication-context", Namespace = "")]
    public class AuthenticationContext
    {
        [DataMember(Name = "username")]
        public string UserName { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "validation-factors", EmitDefaultValue = false)]
        public ValidationFactorsList ValidationFactors { get; set; }
    }
}
