namespace Hyperbar;

public class ConfigurationInitializer<TConfiguration>(DefaultConfiguration<TConfiguration> defaults,
    IConfigurationReader<TConfiguration> reader,
    IConfigurationWriter<TConfiguration> writer) :
    IInitializer
    where TConfiguration :
    class,
    new()
{
    public Task InitializeAsync()
    {
        if (!reader.TryRead(out TConfiguration? _))
        {
            if (defaults.Configuration is not null)
            {
                writer.Write(defaults.Configuration);
            }
        }

        return Task.CompletedTask;
    }
}
