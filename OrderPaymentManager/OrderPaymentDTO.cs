using System;
using System.ComponentModel.DataAnnotations;
using Core.Enum;
using Newtonsoft.Json;

namespace Core.Manager.OrderPaymentManager
{
    public class OrderPaymentDTO
    {
        [JsonProperty("orderPaymentId")]
        public long OrderPaymentId { get; set; }

        [JsonProperty("orderPaymentCode")]
        public string OrderPaymentCode { get; set; }

        [JsonProperty("orderPaymentDate")]
        public System.DateTime OrderPaymentDate { get; set; }

        [JsonProperty("paymentStatus")]
        public ChangeState PaymentStatus { get; set; }

        [JsonProperty("paymentMethod")]
        public int? PaymentMethod { get; set; }

        [Range(typeof(Decimal), "0", "9999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        [JsonProperty("payment")]
        public double Payment { get; set; }

        [JsonProperty("cashback")]
        public Nullable<double> Cashback { get; set; }

        [JsonProperty("grandTotal")]
        public Nullable<double> GrandTotal { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }


        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        [JsonProperty("createdBy")]
        public long CreatedBy { get; set; }

        [JsonProperty("createdDate")]
        public System.DateTime CreatedDate { get; set; }

        [JsonProperty("updatedBy")]
        public long UpdatedBy { get; set; }

        [JsonProperty("updateDate")]
        public System.DateTime UpdatedDate { get; set; }
    }

    public class OnHoldChangeDTO
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cashback")]
        public double? Cashback { get; set; }
    }
}
