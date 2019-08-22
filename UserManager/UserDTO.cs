using System;
using Newtonsoft.Json;

namespace Core.Manager.UserManager
{
    public class UserDTO
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

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

        [JsonProperty("personId")]
        public Nullable<long> PersonId { get; set; }

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

    public class NewPassword
    {
        [JsonProperty("oldPass")]
        public string OldPass { get; set; }

        [JsonProperty("newPass")]
        public string NewPass { get; set; }
    }
}
