using System.Threading.Tasks;
using RestEase;

namespace SubtitlesTranslator.YandexApi
{
    public interface IYandexTranslateApi
    {
        [Post("translate")]
        Task<TranslateResponse> Translate([Body] TranslateRequest request);
    }
}