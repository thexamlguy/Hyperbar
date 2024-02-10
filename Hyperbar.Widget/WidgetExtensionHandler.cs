using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

public class WidgetExtensionHandler(IServiceProvider provider,
    IPublisher publisher,
    IProxyServiceCollection<IWidgetBuilder> typedServices) :
    INotificationHandler<Create<WidgetExtension>>
{
    public async Task Handle(Create<WidgetExtension> notification,
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
            await publisher.PublishAsync(new Create<IWidgetHost>(host),
                cancellationToken);
        }
    }
}
