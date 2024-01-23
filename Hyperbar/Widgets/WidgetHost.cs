using Microsoft.Extensions.Hosting;

namespace Hyperbar;
public class WidgetHost :
    INotificationHandler<Changed<WidgetAvailability>>,
    IWidgetHost
{
    private readonly IHost host;
    private readonly IMediator mediator;
    private readonly IEnumerable<IInitializer> initializers;
    private readonly IProxyService<IMediator> proxyMediator;
    private readonly IConfigurationInitializer<WidgetConfiguration> configurationInitializer;

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

    public Task Handle(Changed<WidgetAvailability> notification,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task InitializeAsync()
    {
        await configurationInitializer.InitializeAsync();
        //foreach (IInitializer initializer in initializers)
        //{
        //    await initializer.InitializeAsync();
        //}

        //if (proxyMediator.Proxy is IMediator mediator)
        //{
        //    await mediator.PublishAsync(new Started<IWidgetHost>(this));
        //}
    }
}