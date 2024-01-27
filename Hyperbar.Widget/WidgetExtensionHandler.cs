using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

public class WidgetExtensionHandler(IProxyServiceCollection<IWidgetBuilder> typedServices,
    IServiceProvider provider,
    IMediator mediator) :
    INotificationHandler<Created<WidgetExtension>>
{
    public async Task Handle(Created<WidgetExtension> notification,
        CancellationToken cancellationToken)
    {
        if(notification.Value is WidgetExtension widgetExtension)
        {
            IWidgetBuilder builder = widgetExtension.Widget.Create();

            builder.ConfigureServices(args =>
            {
                args.AddSingleton(widgetExtension.Assembly);
                args.AddTransient(_ => provider.GetRequiredService<IProxyService<IMediator>>());
                args.AddRange(typedServices.Services);
            });

            IWidgetHost host = builder.Build();
            await host.InitializeAsync();

            await mediator.PublishAsync(new Created<IWidgetHost>(host), 
                cancellationToken);
        }
    }
}
