namespace Handyman.Configuration
{
    public interface IConfigurationSource
    {
        string this[string key] { get; }
    }
}