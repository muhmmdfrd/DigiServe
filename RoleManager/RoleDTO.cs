using System;
using Newtonsoft.Json;

namespace Core.Manager.RoleManager
{
    public class RoleDTO
    {
        [JsonProperty("roleId")]
        public long RoleId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public Nullable<int> Status { get; set; }

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
