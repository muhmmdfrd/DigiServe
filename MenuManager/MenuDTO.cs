using Newtonsoft.Json;
using System;

namespace Core.Manager.MenuManager
{
    public class MenuDTO
    {
        [JsonProperty("menuId")]
        public long MenuId { get; set; }

        [JsonProperty("menuCode")]
        public string MenuCode { get; set; }

        [JsonProperty("menuType")]
        public Nullable<int> MenuType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("createdBy")]
        public Nullable<int> CreatedBy { get; set; }

        [JsonProperty("createDate")]
        public Nullable<System.DateTime> CreateDate { get; set; }

        [JsonProperty("updateBy")]
        public Nullable<int> UpdatedBy { get; set; }

        [JsonProperty("updateDate")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [JsonProperty("price")]
        public Nullable<double> Price { get; set; }
    }
}
