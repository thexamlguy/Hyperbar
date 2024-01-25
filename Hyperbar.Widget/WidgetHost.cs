﻿using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

public class WidgetHost :
    INotificationHandler<Changed<WidgetAvailability>>,
    IWidgetHost
{
    private readonly IConfigurationInitializer<WidgetConfiguration> configurationInitializer;
    private readonly IServiceProvider services;
    private readonly IEnumerable<IInitializer> initializers;
    private readonly IMediator mediator;
    private readonly IProxyService<IMediator> proxyMediator;

    public WidgetHost(IServiceProvider services,
        IMediator mediator,
        IEnumerable<IInitializer> initializers,
        IProxyService<IMediator> proxyMediator,
        IConfigurationInitializer<WidgetConfiguration> configurationInitializer)
    {
        this.services = services;
        this.mediator = mediator;
        this.initializers = initializers;
        this.proxyMediator = proxyMediator;
        this.configurationInitializer = configurationInitializer;

        mediator.Subscribe(this);
    }

    public WidgetConfiguration Configuration => services.GetRequiredService<WidgetConfiguration>();
   
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

    public async Task InitializeAsync() => await configurationInitializer.InitializeAsync();

    private async Task StartAsync()
    {
        if (proxyMediator.Proxy is IMediator mediator)
        {
            await mediator.PublishAsync(new Started<IWidgetHost>(this));
        }

        foreach (IInitializer initializer in initializers)
        {
            await initializer.InitializeAsync();
        }
    }
}