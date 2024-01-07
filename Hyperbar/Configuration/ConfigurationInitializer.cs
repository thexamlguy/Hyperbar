using Microsoft.Extensions.Options;

namespace Hyperbar;

public class ConfigurationInitializer<TConfiguration>(DefaultConfiguration<TConfiguration> defaults,
    IConfigurationWriter<TConfiguration> writer,
    IOptionsMonitor<TConfiguration> options,
    IMediator mediator) : IInitializer
    where TConfiguration :
    class, new()
{
    public Task InitializeAsync()
    {
        options.OnChange(async args =>
        {
            await mediator.PublishAsync(new ConfigurationChanged<TConfiguration>(args));
        });

        if (defaults.Configuration is not null)
        {
            writer.Write(defaults.Configuration);
        }

        return Task.CompletedTask;
    }
}
