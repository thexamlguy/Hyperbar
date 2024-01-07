using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading;

namespace Hyperbar;

public record ConfigurationChanged<TConfiguration>(TConfiguration Configuration) : INotification 
    where TConfiguration : 
    class;

public class ConfigurationInitializer<TConfiguration>(DefaultConfiguration<TConfiguration> defaults,
    IConfigurationWriter<TConfiguration> writer,
    IOptionsMonitor<TConfiguration> options,
    IMediator mediator) : IInitializer
    where TConfiguration :
    class, new()
{
    public Task InitializeAsync()
    {
        options.OnChange(args =>
        {
            mediator.PublishAsync(new ConfigurationChanged<TConfiguration>(args));
        });

        writer.Write(defaults.Configuration);
        return Task.CompletedTask;
    }
}
