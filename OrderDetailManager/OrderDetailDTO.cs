using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Core.Manager
{
    public class OrderDetailDTO
    {
        [JsonProperty("orderDetailId")]
        public long OrderDetailId { get; set; }

        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        [JsonProperty("menuCode")]
        public string MenuCode { get; set; }

        [JsonProperty("menuType")]
        public int? MenuType { get; set; }

        [JsonProperty("address")]
        public string Adress { get; set; }

        [JsonProperty("userId")]
        public long UserId { get; set; }
        
        [JsonProperty("imei")]
        public string IMEI { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("menu")]
        public string Menu { get; set; }

        [JsonProperty("price")]
        public double? Price { get; set; }

        [JsonProperty("totalPrice")]
        public Nullable<double> TotalPrice { get; set; }

        [JsonProperty("qty")]
        public int? Qty { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("createdBy")]
        public Nullable<int> CreatedBy { get; set; }

        [JsonProperty("createdDate")]
        public Nullable<System.DateTime> CreateDate { get; set; }

        [JsonProperty("updatedBy")]
        public Nullable<int> UpdatedBy { get; set; }

        [JsonProperty("updatedDate")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }


    public class OrderDetailMenuDTO
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("orderDetailId")]
        public long? OrderDetailId { get; set; }

        [JsonProperty("menu")]
        public string Menu { get; set; }

        [JsonProperty("menuCode")]
        public string MenuCode { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("price")]
        public double? Price { get; set; }

        [JsonProperty("totalPrice")]
        public Nullable<double> TotalPrice { get; set; }

        [JsonProperty("qty")]
        public int? Qty { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }
    }

    public class OrderDetailMenuGroupDTO
    {
        [JsonProperty("menu")]
        public string Menu { get; set; }

        [JsonProperty("price")]
        public double? Price { get; set; }

        [JsonProperty("totalQty")]
        public int? TotalQty { get; set; }

        [JsonProperty("totalPrice")]
        public double? TotalPrice { get; set; }

        [JsonProperty("customer")]
        public List<CustomerDTO> Customer { get; set; }
    }

    public class CustomerDTO
    {
        [JsonProperty("userId")]
        public long? UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("qty")]
        public int? Qty { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonIgnore]
        public string MenuCode { get; set; }
    }
}
