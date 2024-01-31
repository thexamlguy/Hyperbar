using Microsoft.Extensions.Hosting;

namespace Hyperbar.Widget;

public class WidgetService : 
    IHostedService
{
    private readonly IEnumerable<IInitialization> initializers;

    public WidgetService(IEnumerable<IInitialization> initializers)
    {
        this.initializers = initializers;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (IInitialization initializer in initializers)
        {
            await initializer.InitializeAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
