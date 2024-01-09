namespace Hyperbar;

public class ConfigurationMonitor<TConfiguration> : IInitializer
    where TConfiguration :
    class, new()
{
    private readonly FileSystemWatcher watcher;

    public ConfigurationMonitor(IConfigurationReader<TConfiguration> reader)
    {
        this.watcher = new FileSystemWatcher();
    }

    public Task InitializeAsync()
    {
        void ChangedHandler(object sender,
            FileSystemEventArgs args)
        {

        }

        watcher.Changed += ChangedHandler;
        return Task.CompletedTask;
    }
}
