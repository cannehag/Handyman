using System.Collections.Generic;

namespace Handyman.Configuration
{
    public class ConfigurationReader : IConfigurationReader
    {
        public ConfigurationReader() { }

        public ConfigurationReader(IEnumerable<IConfigurationSource> configurationSources)
        {
            foreach (var configurationSource in configurationSources)
            {
                Sources.Add(configurationSource);
            }
        }

        public string this[string key]
        {
            get
            {
                string value = null;
                for (var i = 0; value == null && i < Sources.Count; i++)
                    value = Sources[i][key];
                return value;
            }
        }

        public IList<IConfigurationSource> Sources { get; } = new List<IConfigurationSource>();
    }
}
