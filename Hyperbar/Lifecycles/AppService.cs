using Microsoft.Extensions.Hosting;
using System;

namespace Hyperbar;

public class AppService(IEnumerable<IInitializer> initializers) :
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