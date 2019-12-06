using System.Linq;
using System.Reflection;
using Configgy.Source;
using Microsoft.Extensions.Configuration;

namespace SubtitlesTranslator.Core.Configuration
{
    internal sealed class CustomConfigurationRootSource : ValueSourceAttributeBase
    {
        private IConfigurationRoot ConfigurationRoot { get; }

        public CustomConfigurationRootSource(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
        }

        public override bool Get(string valueName, PropertyInfo property, out string value)
        {
            PropertyInfo propertyInfo = property;
            var sectionAttribute =
                propertyInfo?.DeclaringType?.GetCustomAttributes(true).OfType<ConfigurationRootSectionAttribute>().SingleOrDefault(); 
            
            IConfiguration configuration = ConfigurationRoot;
            
            if (sectionAttribute?.Prefixes != null)
            {
                foreach (string prefix in sectionAttribute.Prefixes)
                {
                    configuration = configuration.GetSection(prefix);
                    if (configuration == null)
                    {
                        value = null;
                        return false;
                    }
                }
            }
            
            value = configuration[valueName];
            return value != null;
        }
    }
}