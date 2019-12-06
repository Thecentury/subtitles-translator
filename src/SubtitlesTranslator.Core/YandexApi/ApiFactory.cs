using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestEase;

namespace SubtitlesTranslator.Core.YandexApi
{
    public static class ApiFactory
    {
        public static IYandexTranslateApi CreateTranslateApi(string token)
        {
            return RestClient.For<IYandexTranslateApi>("https://translate.api.cloud.yandex.net/translate/v2",
                (request, cancellationToken) =>
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    return Task.CompletedTask;
                });
        }

        public static IYandexIamTokensApi CreateIamApi() 
            => RestClient.For<IYandexIamTokensApi>("https://iam.api.cloud.yandex.net/iam/v1");
    }
}