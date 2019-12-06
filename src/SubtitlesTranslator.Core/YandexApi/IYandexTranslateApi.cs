using System.Threading.Tasks;
using RestEase;

namespace SubtitlesTranslator.Core.YandexApi
{
    public interface IYandexTranslateApi
    {
        [Post("translate")]
        Task<TranslateResponse> Translate([Body] TranslateRequest request);
    }
}