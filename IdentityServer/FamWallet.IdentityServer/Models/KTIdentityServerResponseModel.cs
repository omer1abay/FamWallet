using System.Text.Json.Serialization;

namespace FamWallet.IdentityServer.Models
{
    public class KTIdentityServerResponseModel
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("expires_in")]
        public int ExpireTime { get; set; }
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
        [JsonPropertyName("scope")]
        public string Scopes { get; set; }
    }
}
