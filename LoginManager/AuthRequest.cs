using Newtonsoft.Json;

namespace Core.Manager.LoginManager
{
    public class AuthRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("imei")]
        public string IMEI { get; set; }
    }
}