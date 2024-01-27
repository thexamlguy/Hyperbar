using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

public class WidgetHost :
    INotificationHandler<Changed<WidgetAvailability>>,
    IWidgetHost
{
    private readonly IEnumerable<IInitializer> initializers;
    private readonly IMediator mediator;
    private readonly IProxyService<IMediator> proxyMediator;
    private readonly IServiceProvider services;

    public WidgetHost(IServiceProvider services,
        IMediator mediator,
        IEnumerable<IInitializer> initializers,
        IProxyService<IMediator> proxyMediator)
    {
        this.services = services;
        this.mediator = mediator;
        this.initializers = initializers;
        this.proxyMediator = proxyMediator;

        mediator.Subscribe(this);
    }

    public WidgetConfiguration Configuration => 
        services.GetRequiredService<WidgetConfiguration>();
   
    public IServiceProvider Services => services;

    public async Task Handle(Changed<WidgetAvailability> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Value is WidgetAvailability widgetAvailability)
        {
            if (widgetAvailability.Value)
            {
                await StartAsync();
            }
        }
    }

    public async Task InitializeAsync()
    {
        foreach (IInitializer initializer in initializers)
        {
            await initializer.InitializeAsync();
        }
    }

    private async Task StartAsync()
    {
        if (proxyMediator.Proxy is IMediator mediator)
        {
            await mediator.PublishAsync(new Started<IWidgetHost>(this));
        }
    }
}