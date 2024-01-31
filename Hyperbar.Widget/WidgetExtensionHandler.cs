using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

public class WidgetExtensionHandler(IServiceProvider provider,
    IMediator mediator,
    IProxyServiceCollection<IWidgetBuilder> typedServices) :
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
            await mediator.PublishAsync(new Created<IWidgetHost>(host),
                cancellationToken);
        }
    }
}
