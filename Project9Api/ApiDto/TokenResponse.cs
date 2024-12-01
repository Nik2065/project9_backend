using System.Text.Json.Serialization;

namespace Project9Api.ApiDto
{
    public class TokenResponse : BaseResponse
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }

        [JsonPropertyName("login")]
        public string? Login { get; set; }
    }
}
