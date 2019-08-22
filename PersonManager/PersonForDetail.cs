using Newtonsoft.Json;

namespace Core.Manager.PersonManager
{
    public class PersonForDetail
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
