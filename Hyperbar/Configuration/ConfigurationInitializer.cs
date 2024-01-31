namespace Hyperbar;

public class ConfigurationInitializer<TConfiguration>(IConfigurationMonitor<TConfiguration> monitor,
    IConfigurationReader<TConfiguration> reader, 
    IConfigurationWriter<TConfiguration> writer,
    IConfigurationFactory<TConfiguration> factory) :
    IConfigurationInitializer<TConfiguration>,
    IInitialization
    where TConfiguration :
    class
{
    public async Task InitializeAsync()
    {
        if (!reader.TryRead(out TConfiguration? _))
        {
            if (factory.Create() is object defaultConfiguration)
            {
                writer.Write(defaultConfiguration);
            }
        }

        await monitor.InitializeAsync();
    }
}
