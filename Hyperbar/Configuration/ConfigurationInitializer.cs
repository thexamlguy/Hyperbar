namespace Hyperbar;

public class ConfigurationInitializer<TConfiguration>(DefaultConfiguration<TConfiguration> defaults,
    IConfigurationWriter<TConfiguration> writer) : IInitializer
    where TConfiguration :
    class, new()
{
    public Task InitializeAsync()
    {
        writer.Write(defaults.Configuration);
        return Task.CompletedTask;
    }
}
