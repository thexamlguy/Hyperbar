using Microsoft.Extensions.Hosting;

namespace Hyperbar.Widget;

public class WidgetService : 
    IHostedService
{
    private readonly IEnumerable<IInitializer> initializers;

    public WidgetService(IEnumerable<IInitializer> initializers)
    {
        this.initializers = initializers;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (IInitializer initializer in initializers)
        {
            await initializer.InitializeAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
