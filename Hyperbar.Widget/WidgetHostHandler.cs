namespace Hyperbar.Widget;

public class WidgetHostHandler(IMediator mediator,
    IFactory<IWidgetHost, WidgetContainerViewModel?> factory) :
    INotificationHandler<Started<IWidgetHost>>,
    INotificationHandler<Stopped<IWidgetHost>>
{
    public async Task Handle(Started<IWidgetHost> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Value is IWidgetHost host)
        {
            if (factory.Create(host) is WidgetContainerViewModel containerViewModel)
            {
                await mediator.PublishAsync(new Created<WidgetContainerViewModel>(containerViewModel), 
                    nameof(WidgetBarViewModel), cancellationToken);
            }
        }
    }

    public Task Handle(Stopped<IWidgetHost> notification,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}