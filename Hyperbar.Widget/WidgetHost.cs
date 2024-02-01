using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Widget;

public sealed class WidgetHost :
    IWidgetHost
{
    private readonly IServiceProvider services;
    private readonly IMediator mediator;
    private readonly IProxyService<IMediator> proxyMediator;

    public WidgetHost(IServiceProvider services,
        IMediator mediator,
        IProxyService<IMediator> proxyMediator)
    {
        this.services = services;
        this.mediator = mediator;
        this.proxyMediator = proxyMediator;

        mediator.Subscribe(this);
    }

    public WidgetConfiguration Configuration =>
        Services.GetRequiredService<WidgetConfiguration>();
   
    public IServiceProvider Services => services;

    public void Dispose()
    {

    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        if (proxyMediator.Proxy is IMediator mediator)
        {
            await mediator.PublishAsync(new Started<IWidgetHost>(this),
                cancellationToken);
        }

        await this.mediator.PublishAsync(new Started<IWidgetHost>(this), 
            cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}