using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Widget;

public sealed class WidgetHost(IServiceProvider services,
    IPublisher publisher,
    IProxyService<IPublisher> proxyPublisher,
    IEnumerable<IHostedService> hostedServices) :
    IWidgetHost
{
    private readonly IPublisher publisher = publisher;

    public WidgetConfiguration Configuration =>
        Services.GetRequiredService<WidgetConfiguration>();
   
    public IServiceProvider Services => services;

    public void Dispose()
    {

    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        foreach (IHostedService service in hostedServices)
        {
            await service.StartAsync(cancellationToken);
        }

        if (proxyPublisher.Proxy is IPublisher publisher)
        {
            await publisher.PublishAsync(new Started<IWidgetHost>(this),
                cancellationToken);
        }

        await this.publisher.PublishAsync(new Started<IWidgetHost>(this), 
            cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        foreach(IHostedService service in hostedServices)
        {
            await service.StopAsync(cancellationToken);
        }
    }
}