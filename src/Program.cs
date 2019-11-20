using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using SubtitlesParser.Classes;

namespace SubtitlesTranslator
{
    internal static class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var cfg = new TranslatorConfig(args);

            var api = TranslateFactory.CreateApi(cfg.YandexTranslateToken);

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
                    Console.WriteLine(exc);
                    return -1;
                }
            }

            var result = SubtitlesToString(items, translations);

            var destFileName = GetDestFileName(cfg);

            File.WriteAllText(destFileName, result);

            Console.WriteLine($"Done. Dest file: '{destFileName}'");

            return 0;
        }

        private static string GetDestFileName(TranslatorConfig cfg)
        {
            string dir = Path.GetDirectoryName(cfg.SourceFile);
            string fileName = Path.GetFileNameWithoutExtension(cfg.SourceFile);

            string destFileName = Path.Combine(dir, $"{fileName}-translated-{cfg.TargetLanguageCode}.srt");
            return destFileName;
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