using Newtonsoft.Json;

namespace Core.Manager.LoginManager
{
    public enum CodeStatus
    {
        Success = 200,
        Unauthorized = 401,
        ValidationError = 400
    }

    public class AuthResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
