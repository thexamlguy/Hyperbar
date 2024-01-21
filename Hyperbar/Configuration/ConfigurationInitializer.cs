namespace Hyperbar;

public class ConfigurationInitializer<TConfiguration>(IConfigurationReader<TConfiguration> reader, 
    IConfigurationWriter<TConfiguration> writer,
    IConfigurationFactory<TConfiguration> factory) :
    IConfigurationInitializer<TConfiguration>,
    IInitializer
    where TConfiguration :
    class
{
    public Task InitializeAsync()
    {
        if (!reader.TryRead(out TConfiguration? _))
        {
            if (factory.Create() is TConfiguration configuration)
            {
                writer.Write(configuration);
            }
        }

        return Task.CompletedTask;
    }
}
