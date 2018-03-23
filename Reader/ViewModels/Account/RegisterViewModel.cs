using Newtonsoft.Json;

namespace Reader.ViewModels.Account
{
    public class RegisterViewModel
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("confirmPassword")]
        public string ConfirmPassword { get; set; }

    }
}
