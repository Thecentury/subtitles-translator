using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using SubtitlesParser.Classes;
using SubtitlesTranslator.Core.Configuration;
using SubtitlesTranslator.Core.YandexApi;

namespace SubtitlesTranslator.Core
{
    public class Translator
    {
        public async Task<TranslationResult> TranslateAsync(TranslatorConfig cfg)
        {
            var apiToken = cfg.YandexTranslateToken;
            if (apiToken == null && cfg.YandexTranslateOAuthToken != null)
            {
                var yandexIamTokensApi = ApiFactory.CreateIamApi();
                apiToken = await yandexIamTokensApi.CreateToken(cfg.YandexTranslateOAuthToken);
            }

            if (string.IsNullOrEmpty(apiToken))
            {
                return TranslationResult.FromError("Error: Yandex Translate api token is not provided.");
            }

            var api = ApiFactory.CreateTranslateApi(apiToken);

            var parser = new SubtitlesParser.Classes.Parsers.SrtParser();
            await using var fileStream = File.OpenRead(cfg.SourceFile);

            var items = parser.ParseStream(fileStream, Encoding.Default);
            var allStrings = items.SelectMany(x => x.Lines).Distinct().ToList();

            var translations = new Dictionary<string, string>();
            foreach (var batch in allStrings.Batch(10))
            {
                var src = batch.ToList();
                try
                {
                    var translateResponse = await api.Translate(new TranslateRequest
                    {
                        folder_id = cfg.YandexCloudFolderId,
                        targetLanguageCode = cfg.TargetLanguageCode,
                        texts = src
                    });

                    for (var i = 0; i < src.Count; i++)
                    {
                        var dst = translateResponse.Translations[i].Text;
                        dst = FixTags(dst);
                        translations.Add(src[i], dst);
                    }
                }
                catch (Exception exc)
                {
                    return TranslationResult.FromError(exc.ToString());
                }
            }

            var translation = SubtitlesToString(items, translations);

            return TranslationResult.Ok(translation);
        }

        private static string SubtitlesToString(IEnumerable<SubtitleItem> items,
            IReadOnlyDictionary<string, string> translations)
        {
            int index = 1;
            var result = new StringBuilder();
            foreach (var subtitleItem in items)
            {
                result.AppendLine(index.ToString());
                result.AppendLine(subtitleItem.Header());

                foreach (var line in subtitleItem.Lines)
                {
                    if (!translations.TryGetValue(line, out var translation))
                    {
                        translation = $"UNTRANSLATED '{line}'";
                    }

                    result.AppendLine(translation);
                }

                result.AppendLine();

                index++;
            }

            return result.ToString();
        }

        private static string FixTags(string dst)
        {
            return dst
                .Replace("< / i>", "</i>")
                .Replace("</ i>", "</i>")
                .Replace("< /i>", "</i>")
                .Replace("<я>", "<i>")
                .Replace("< i>", "<i>")
                .Replace("<I>", "<i>")
                .Replace("</я>", "</i>");
        }
    }
}