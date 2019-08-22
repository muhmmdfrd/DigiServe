using Newtonsoft.Json;

namespace MiniMenu.Models
{
    public class ApiResponseCheckOrder<T> where T : class
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("messageCode")]
        public int MessageCode { get; set; }

        [JsonProperty("errorStatus")]
        public bool ErrorStatus { get; set; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("orderId")]
        public long Id { get; set; }
    }
}