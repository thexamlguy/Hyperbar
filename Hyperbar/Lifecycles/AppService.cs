using Microsoft.Extensions.Hosting;

namespace Hyperbar;

public class AppService(IEnumerable<IInitialization> initializers) :
    IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (IInitialization initializer in initializers)
        {
            await initializer.InitializeAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}