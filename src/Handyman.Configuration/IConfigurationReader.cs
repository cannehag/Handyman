using System.Collections.Generic;

namespace Handyman.Configuration
{
    public interface IConfigurationReader
    {
        string this[string key] { get; }
        IList<IConfigurationSource> Sources { get; }
    }
}