using Core.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Core.Manager.OrderManager
{

    public class OrderDTO
    {
        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        [JsonProperty("orderCode")]
        public string OrderCode { get; set; }

        [JsonProperty("status")]
        public OrderState Status { get; set; }

        [JsonProperty("orderDate")]
        public Nullable<System.DateTime> OrderDate { get; set; }

        [JsonProperty("createdBy")]
        public Nullable<int> CreatedBy { get; set; }

        [JsonProperty("createdDate")]
        public Nullable<System.DateTime> CreateDate { get; set; }

        [JsonProperty("updatedBy")]
        public Nullable<int> UpdatedBy { get; set; }

        [JsonProperty("updatedDate")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [JsonProperty("orderDetail")]
        public List<OrderDetailDTO> OrderDetailDTOs { get; set; }
    }
}
