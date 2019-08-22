using Newtonsoft.Json;
using System;

namespace Core.Manager.PersonManager
{
    public class PersonUserDTO
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

        [JsonProperty("userId")]
        public long UserId { get; set; }
        
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("imei")]
        public string IMEI { get; set; }

        [JsonProperty("status")]
        public Nullable<int> Status { get; set; }

        [JsonProperty("roleId")]
        public long RoleId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

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
