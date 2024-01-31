using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Widget;

public sealed class WidgetHost :
    IWidgetHost
{
    private readonly IHost host;
    private readonly IMediator mediator;
    private readonly IProxyService<IMediator> proxyMediator;

    public WidgetHost(IHost host,
        IMediator mediator,
        IProxyService<IMediator> proxyMediator)
    {
        this.host = host;
        this.mediator = mediator;
        this.proxyMediator = proxyMediator;

        mediator.Subscribe(this);
    }

    public WidgetConfiguration Configuration =>
        Services.GetRequiredService<WidgetConfiguration>();
   
    public IServiceProvider Services => host.Services;

    public void Dispose()
    {

    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        await host.StartAsync(cancellationToken);

        if (proxyMediator.Proxy is IMediator mediator)
        {
            await mediator.PublishAsync(new Started<IWidgetHost>(this),
                cancellationToken);
        }

        await this.mediator.PublishAsync(new Started<IWidgetHost>(this), 
            cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        await host.StopAsync(cancellationToken);
    }
}