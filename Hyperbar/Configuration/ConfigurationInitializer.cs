namespace Hyperbar;

public class ConfigurationInitializer<TConfiguration>(IConfigurationMonitor<TConfiguration> monitor,
    IConfigurationReader<TConfiguration> reader, 
    IConfigurationWriter<TConfiguration> writer,
    IConfigurationFactory<TConfiguration> factory) :
    IConfigurationInitializer<TConfiguration>,
    IInitializer
    where TConfiguration :
    class
{
    public async Task InitializeAsync()
    {
        if (!reader.TryRead(out TConfiguration? _))
        {
            if (factory.Create() is TConfiguration configuration)
            {
                writer.Write(configuration);
            }
        }

        await monitor.InitializeAsync();
    }
}
