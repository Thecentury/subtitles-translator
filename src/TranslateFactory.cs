using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestEase;

namespace SubtitlesTranslator
{
    public static class TranslateFactory
    {
        public static IYandexTranslateApi CreateApi(string token)
        {
            return RestClient.For<IYandexTranslateApi>("https://translate.api.cloud.yandex.net/translate/v2",
                (request, cancellationToken) =>
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    return Task.CompletedTask;
                });
        }
    }
}