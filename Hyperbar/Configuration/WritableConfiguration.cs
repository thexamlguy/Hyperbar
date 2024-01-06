using Hyperbar.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Hyperbar;

public class WritableConfiguration<TConfiguration>(IConfigurationWriter<TConfiguration> writer,
    IOptionsMonitor<TConfiguration> options,
    IConfiguration configuration) : 
    IWritableConfiguration<TConfiguration> 
    where TConfiguration : 
    class, new()
{
    public TConfiguration Value => options.CurrentValue;

    public TConfiguration Get(string? name) => options.Get(name);

    public void Write(Action<TConfiguration> updateDelegate, 
        bool reload = true)
    {
        writer.Write(updateDelegate);
        if (reload && configuration is IConfigurationRoot configurationRoot)
        {
            configurationRoot.Reload();
        }
    }
}
