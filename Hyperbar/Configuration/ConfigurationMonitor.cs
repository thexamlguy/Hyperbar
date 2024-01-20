namespace Hyperbar;

public class ConfigurationMonitor<TConfiguration>(IConfigurationFile<TConfiguration> configurationFile,
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

        if (configurationFile.FileInfo.PhysicalPath is { } path)
        {
            string fileName = Path.GetFileName(path);

            watcher = new FileSystemWatcher
            {
                NotifyFilter = NotifyFilters.LastWrite,
                Path = path.Replace(fileName, ""),
                Filter = fileName,
                EnableRaisingEvents = true
            };

            watcher.Changed += ChangedHandler;
        }

        return Task.CompletedTask;
    }
}
