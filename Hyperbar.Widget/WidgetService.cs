using Microsoft.Extensions.Hosting;

namespace Hyperbar.Widget;

public sealed class WidgetService(IEnumerable<IInitializer> initializers) :
    IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (IInitializer initializer in initializers)
        {
            await initializer.InitializeAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}