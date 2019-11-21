using System.Diagnostics.CodeAnalysis;

namespace SubtitlesTranslator.YandexApi
{
    // ReSharper disable once ClassNeverInstantiated.Global
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public sealed class TranslateResponse
    {
        public StringTranslation[] Translations { get; set; }
    }
}