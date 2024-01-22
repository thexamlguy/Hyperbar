namespace Hyperbar;

public class WidgetHostHander :
    INotificationHandler<Started<IWidgetHost>>,
    INotificationHandler<Stopped<IWidgetHost>>
{
    public Task Handle(Started<IWidgetHost> notification,
        CancellationToken cancellationToken)
    {
       // throw new NotImplementedException();

        return Task.CompletedTask;
    }

    public Task Handle(Stopped<IWidgetHost> notification,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}