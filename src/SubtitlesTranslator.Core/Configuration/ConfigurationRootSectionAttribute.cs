using System;

namespace SubtitlesTranslator.Core.Configuration
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class ConfigurationRootSectionAttribute : Attribute
    {
        public string[] Prefixes { get; }

        public ConfigurationRootSectionAttribute(string prefixes)
            : this(prefixes, ".")
        {
        }

        private ConfigurationRootSectionAttribute(string prefixes, string separator)
        {
            Prefixes = prefixes.Split(new[]
            {
                separator
            }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}