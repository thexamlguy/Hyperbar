namespace Hyperbar;

public class ConfigurationInitializer<TConfiguration>(IPublisher publisher,
    IConfigurationMonitor<TConfiguration> monitor,
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
        if (!reader.TryRead(out TConfiguration? configuration))
        {
            if (factory.Create() is object defaultConfiguration)
            {
                configuration = (TConfiguration?)defaultConfiguration;
                writer.Write(defaultConfiguration);
            }
        }

        await publisher.PublishAsync(new Changed<TConfiguration>(configuration));
        await monitor.InitializeAsync();
    }
}
