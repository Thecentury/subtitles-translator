using Configgy;
using Configgy.Cache;
using Configgy.Coercion;
using Configgy.Source;
using Configgy.Transformation;
using Configgy.Validation;
using Microsoft.Extensions.Configuration;

namespace SubtitlesTranslator.Core.Configuration
{
    public sealed class TranslatorConfigProvider : ConfigProvider
    {
        public TranslatorConfigProvider(string[] commandLine, IConfigurationRoot configurationRoot) : base(
            new DictionaryCache(),
            new AggregateSource(
                (IValueSource) new DashedCommandLineSource(commandLine),
                (IValueSource) new EnvironmentVariableSource(),
                new CustomConfigurationRootSource(configurationRoot),
                (IValueSource) new DefaultValueAttributeSource()),
            new AggregateTransformer(),
            new AggregateValidator(),
            new AggregateCoercer())
        {
        }
    }
}