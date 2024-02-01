namespace Hyperbar;

public class ConfigurationInitializer<TConfiguration>(IMediator mediator,
    IConfigurationMonitor<TConfiguration> monitor,
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
        if (!reader.TryRead(out TConfiguration? configuration))
        {
            if (factory.Create() is object defaultConfiguration)
            {
                configuration = (TConfiguration?)defaultConfiguration;
                writer.Write(defaultConfiguration);
            }
        }

        await mediator.PublishAsync(new Changed<TConfiguration>(configuration));
        await monitor.InitializeAsync();
    }
}
