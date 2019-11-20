using System.ComponentModel;
using Configgy;

namespace SubtitlesTranslator
{
    public sealed class TranslatorConfig : Config
    {
        public TranslatorConfig(string[] commandLine) : base(commandLine)
        {
        }

        public string YandexTranslateToken => Get<string>();
        
        public string YandexCloudFolderId => Get<string>();
        
        [DefaultValue("ru")]
        public string TargetLanguageCode => Get<string>();
        
        public string SourceFile => Get<string>();
    }
}