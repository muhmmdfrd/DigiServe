using Newtonsoft.Json;

namespace MiniMenu.Models
{
    public class UserForOrderDetail
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
        
        [JsonProperty("imei")]
        public string IMEI { get; set; }
    }
}