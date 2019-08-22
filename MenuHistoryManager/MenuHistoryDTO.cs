using Newtonsoft.Json;
using System;

namespace Core.Manager.MenuHistoryManager
{

   
    public class MenuHistoryDTO
    {
        [JsonProperty("menuHistoryId")]
        public long MenuHistoryId { get; set; }

        [JsonProperty("menuCode")]
        public string MenuCode { get; set; }

        [JsonProperty("menuType")]
        public int? MenuType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("price")]
        public double? Price { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("createdDate")]
        public System.DateTime? CreatedDate { get; set; }

        [JsonProperty("updatedBy")]
        public long? UpdatedBy { get; set; }

        [JsonProperty("updatedDate")]
        public System.DateTime? UpdatedDate { get; set; }
    }
}
