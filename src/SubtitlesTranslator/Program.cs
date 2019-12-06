using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SubtitlesTranslator.Core;
using SubtitlesTranslator.Core.Configuration;

namespace SubtitlesTranslator
{
    internal static class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();

            var configurationRoot = configurationBuilder
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile("appsettings.local.json", true)
                .Build();

            var cfg = new TranslatorConfig(new TranslatorConfigProvider(args, configurationRoot));

            var translationResult = await new Translator().TranslateAsync(cfg);

            if (translationResult.Error != null)
            {
                Console.WriteLine(translationResult.Error);
                return -1;
            }

            var destFileName = GetDestFileName(cfg);

            File.WriteAllText(destFileName, translationResult.Translation);

            Console.WriteLine($"Done. Destination file: '{destFileName}'");

            return 0;
        }

        private static string GetDestFileName(TranslatorConfig cfg)
        {
            string dir = Path.GetDirectoryName(cfg.SourceFile);
            string fileName = Path.GetFileNameWithoutExtension(cfg.SourceFile);

            string destFileName = Path.Combine(dir, $"{fileName}-translated-{cfg.TargetLanguageCode}.srt");
            return destFileName;
        }
    }
}