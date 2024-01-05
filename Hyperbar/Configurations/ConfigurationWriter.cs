using Microsoft.Extensions.Configuration;

namespace Hyperbar.Configurations;

public class ConfigurationWriter<TConfiguration>(IConfiguration rootConfiguration) :
    IConfigurationWriter<TConfiguration> where TConfiguration : class, new()
{
    public void Write(string section, TConfiguration configuration)
    {
        if (rootConfiguration is IConfigurationRoot root)
        {
            foreach (IConfigurationProvider? provider in root.Providers)
            {
                if (provider is IWritableConfigurationProvider writableConfigurationProvider)
                {
                    writableConfigurationProvider.Write(section, configuration);
                }
            }
        }
    }
}