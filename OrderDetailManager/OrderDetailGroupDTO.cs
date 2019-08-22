using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.Manager.OrderDetailManager
{
    public class OrderDetailGroupDTO
    {
        [JsonProperty("userId")]
        public long? UserId { get; set; }

        [JsonProperty("customer")]
        public string NameCustomer { get; set; }

        [JsonProperty("menuOrdered")]
        public List<OrderDetailMenuDTO> MenuOrdered { get; set; }

        [JsonProperty("payment")]
        public double? Payment { get; set; }

        [JsonProperty("grandTotal")]
        public double? GrandTotal { get; set; }

        [JsonProperty("cashback")]
        public double? Cashback { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }
    }
}
