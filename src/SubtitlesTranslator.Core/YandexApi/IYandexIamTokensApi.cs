using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;

namespace SubtitlesTranslator.Core.YandexApi
{
    public interface IYandexIamTokensApi
    {
        [Post("tokens")]
        Task<TokensResponse> CreateToken([Body(BodySerializationMethod.Default)]
            Dictionary<string, string> data);
    }
}