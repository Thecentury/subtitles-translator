namespace SubtitlesTranslator.Core
{
    public sealed class TranslationResult
    {
        public string Translation { get; }
        public string Error { get; }

        private TranslationResult(string translation, string error)
        {
            Translation = translation;
            Error = error;
        }

        public static TranslationResult Ok(string translation)
            => new TranslationResult(translation, null);

        public static TranslationResult FromError(string error)
            => new TranslationResult(null, error);
    }
}