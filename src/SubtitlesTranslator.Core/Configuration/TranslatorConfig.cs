using System.ComponentModel;
using Configgy;

namespace SubtitlesTranslator.Core.Configuration
{
    [ConfigurationRootSection("Translator")]
    public sealed class TranslatorConfig : Config
    {
        public TranslatorConfig(IConfigProvider configProvider) : base(configProvider)
        {
        }

        [DefaultValue(null)] public string YandexTranslateToken => Get<string>();

        [DefaultValue(null)] public string YandexTranslateOAuthToken => Get<string>();

        public string YandexCloudFolderId => Get<string>();

        [DefaultValue("ru")] public string TargetLanguageCode => Get<string>();

        public string SourceFile => Get<string>();
    }
}