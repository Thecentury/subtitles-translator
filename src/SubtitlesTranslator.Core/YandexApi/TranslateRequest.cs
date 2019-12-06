using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SubtitlesTranslator.Core.YandexApi
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public sealed class TranslateRequest
    {
        public string targetLanguageCode { get; set; }

        public string folder_id { get; set; }

        public List<string> texts { get; set; }
    }
}