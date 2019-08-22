using Newtonsoft.Json;
using System;

namespace Core.Manager.OrderManager
{
    public class JustDate
    {
        [JsonProperty("createdDate")]
        public DateTime? CreateDate { get; set; }
    }
}
