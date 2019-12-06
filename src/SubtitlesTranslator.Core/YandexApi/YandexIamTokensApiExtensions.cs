using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubtitlesTranslator.Core.YandexApi
{
    public static class YandexIamTokensApiExtensions
    {
        public static async Task<string> CreateToken(this IYandexIamTokensApi api, string oauthToken)
        {
            var tokensResponse = await api.CreateToken(new Dictionary<string, string>
            {
                {"yandexPassportOauthToken", oauthToken}
            });

            return tokensResponse.IamToken;
        }
    }
}