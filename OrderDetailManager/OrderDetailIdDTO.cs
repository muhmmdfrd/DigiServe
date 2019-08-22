using Newtonsoft.Json;
using System;

namespace Core.Manager.OrderDetailManager
{
    public class OrderDetailIdDTO
    {
        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        [JsonProperty("orderDetailId")]
        public long OrderDetailId { get; set; }

        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("menu")]
        public string Menu { get; set; }

        [JsonProperty("menuType")]
        public int? MenuType { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("price")]
        public double? Price { get; set; }

        [JsonProperty("qty")]
        public int? Qty { get; set; }

        [JsonProperty("totalPrice")]
        public Double? TotalPrice { get; set; }

        [JsonProperty("createdBy")]
        public long CreatedBy { get; set; }

        [JsonProperty("createdDate")]
        public DateTime? CreatedDate { get; set; }

        [JsonProperty("updatedBy")]
        public long UpdatedBy { get; set; }

        [JsonProperty("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
