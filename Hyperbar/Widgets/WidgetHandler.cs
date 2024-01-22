using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public class WidgetHandler(IProxyServiceCollection<IWidgetBuilder> typedServices,
    IServiceProvider provider,
    IMediator mediator) :
    INotificationHandler<Created<IWidget>>
{
    public async Task Handle(Created<IWidget> notification,
        CancellationToken cancellationToken)
    {
        if(notification.Value is IWidget widget)
        {
            IWidgetBuilder builder =  widget.Create();

            builder.ConfigureServices(args =>
            {
                args.AddTransient(_ => provider.GetRequiredService<IProxyService<IMediator>>());
                args.AddRange(typedServices.Services);
            });

            IWidgetHost host = builder.Build();

            await host.StartAsync();
            await mediator.PublishAsync(new Created<IWidgetHost>(host), 
                cancellationToken);
        }
    }
}
