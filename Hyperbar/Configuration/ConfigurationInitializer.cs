namespace Hyperbar;

public class ConfigurationInitializer<TConfiguration>(DefaultConfiguration<TConfiguration> defaults,
    IConfigurationWriter<TConfiguration> writer) :
    IInitializer
    where TConfiguration :
    class,
    new()
{
    public Task InitializeAsync()
    {
        if (defaults.Configuration is not null)
        {
            writer.Write(defaults.Configuration);
        }

        return Task.CompletedTask;
    }
}
