using Newtonsoft.Json;

namespace SADM.Models.Responses
{
    public class GetAppTokenResponse : ResponseBase
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public double ExpiresIn { get; set; }
    }
}
