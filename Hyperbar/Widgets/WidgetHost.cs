using Microsoft.Extensions.Hosting;

namespace Hyperbar;

public class WidgetHost :
    INotificationHandler<Changed<WidgetAvailability>>,
    IWidgetHost
{
    private readonly IConfigurationInitializer<WidgetConfiguration> configurationInitializer;
    private readonly IHost host;
    private readonly IEnumerable<IInitializer> initializers;
    private readonly IMediator mediator;
    private readonly IProxyService<IMediator> proxyMediator;

    public WidgetHost(IHost host,
        IMediator mediator,
        IEnumerable<IInitializer> initializers,
        IProxyService<IMediator> proxyMediator,
        IConfigurationInitializer<WidgetConfiguration> configurationInitializer)
    {
        this.host = host;
        this.mediator = mediator;
        this.initializers = initializers;
        this.proxyMediator = proxyMediator;
        this.configurationInitializer = configurationInitializer;

        mediator.Subscribe(this);
    }

    public IServiceProvider Services => host.Services;

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

    public async Task InitializeAsync() => await configurationInitializer.InitializeAsync();

    private async Task StartAsync()
    {
        foreach (IInitializer initializer in initializers)
        {
            await initializer.InitializeAsync();
        }


        //if (proxyMediator.Proxy is IMediator mediator)
        //{
        //    await mediator.PublishAsync(new Started<IWidgetHost>(this));
        //}
    }
}