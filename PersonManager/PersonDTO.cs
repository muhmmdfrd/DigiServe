using System;
using Newtonsoft.Json;

namespace Core.Manager.PersonManager
{
    public class PersonDTO
    {
        [JsonProperty("personId")]
        public long PersonId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public Nullable<int> Gender { get; set; }

        [JsonProperty("dateOfBirth")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("createdBy")]
        public int CreatedBy { get; set; }

        [JsonProperty("createdDate")]
        public System.DateTime CreatedDate { get; set; }

        [JsonProperty("updatedBy")]
        public int UpdatedBy { get; set; }

        [JsonProperty("updatedDate")]
        public System.DateTime UpdatedDate { get; set; }
    }
}
