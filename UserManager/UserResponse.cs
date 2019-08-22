using Newtonsoft.Json;

namespace Core.Manager.UserManager
{
    public class UserResponse<T> where T : class
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
    }
}
