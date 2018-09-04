using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CrowdRestClient.Models
{
    [DataContract]
    public class ValidationFactor
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }

    [DataContract]
    public class ValidationFactorsList
    {
        [DataMember(Name = "validationFactors")]
        public List<ValidationFactor> ValidationFactors { get; set; }
    }
}
