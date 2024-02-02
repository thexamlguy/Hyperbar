using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Widget;

public sealed class WidgetHost :
    IWidgetHost
{
    private readonly IServiceProvider services;
    private readonly IMediator mediator;
    private readonly IProxyService<IMediator> proxyMediator;
    private readonly IEnumerable<IHostedService> hostedServices;

    public WidgetHost(IServiceProvider services,
        IMediator mediator,
        IProxyService<IMediator> proxyMediator,
        IEnumerable<IHostedService> hostedServices)
    {
        this.services = services;
        this.mediator = mediator;
        this.proxyMediator = proxyMediator;
        this.hostedServices = hostedServices;
    }

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
        foreach(IHostedService service in hostedServices)
        {
            await service.StopAsync(cancellationToken);
        }
    }
}