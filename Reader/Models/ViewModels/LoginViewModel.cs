using Newtonsoft.Json;

namespace Reader.Models.ViewModels
{
    public class LoginViewModel
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("remember")]
        public bool Remember{ get; set; }
    }
}
