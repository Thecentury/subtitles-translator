using Newtonsoft.Json;

namespace SubtitlesTranslator.Core.YandexApi
{
    public sealed class TokensResponse
    {
        [JsonProperty("iamToken")]
        public string IamToken { get; set; }
        
        [JsonProperty("expiresAt")]
        public string ExpiresAt { get; set; }
    }
}