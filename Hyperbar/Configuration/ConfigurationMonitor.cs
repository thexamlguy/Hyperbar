namespace Hyperbar;

public class ConfigurationMonitor<TConfiguration>(IConfigurationSource<TConfiguration> source,
    IConfigurationReader<TConfiguration> reader,
    IMediator mediator) : IInitializer
    where TConfiguration :
    class, new()
{
    private FileSystemWatcher? watcher;

    public Task InitializeAsync()
    {
        async void ChangedHandler(object sender,
            FileSystemEventArgs args)
        {
            if (reader.Read() is { } configuration)
            {
                await mediator.PublishAsync(new ConfigurationChanged<TConfiguration>(configuration));
            }
        }

        string fileName = Path.GetFileName(source.Path);

        watcher = new FileSystemWatcher
        {
            NotifyFilter = NotifyFilters.LastWrite,
            Path = source.Path.Replace(fileName, ""),
            Filter = fileName,
            EnableRaisingEvents = true
        };
        watcher.Changed += ChangedHandler;
        return Task.CompletedTask;
    }
}
