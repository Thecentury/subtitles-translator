using System.Diagnostics.CodeAnalysis;

namespace SubtitlesTranslator.Core.YandexApi
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public sealed class StringTranslation
    {
        public string Text { get; set; }

        public string DetectedLanguageCode { get; set; }
    }
}