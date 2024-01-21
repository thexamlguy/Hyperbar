namespace Hyperbar;

public class ConfigurationInitializer<TConfiguration>(IConfigurationReader<TConfiguration> reader) :
    IInitializer
    where TConfiguration :
    class
{
    public Task InitializeAsync()
    {
        reader.Read();
        return Task.CompletedTask;
    }
}
