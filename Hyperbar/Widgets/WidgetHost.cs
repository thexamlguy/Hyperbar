using Microsoft.Extensions.Hosting;

namespace Hyperbar;
public class WidgetHost(IHost host,
    IProxyService<IMediator> proxyMediator) :
    IWidgetHost
{
    public IServiceProvider Services => host.Services;

    public async Task StartAsync()
    {
        if (proxyMediator.Proxy is IMediator mediator)
        {
            await mediator.PublishAsync(new Started<IWidgetHost>(this));
        }
    }

    public async Task StopAsync()
    {
        if (proxyMediator.Proxy is IMediator mediator)
        {
            await mediator.SendAsync(new Started<IWidgetHost>(this));
        }
    }
}