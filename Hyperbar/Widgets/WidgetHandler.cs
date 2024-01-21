namespace Hyperbar;

public class WidgetHandler(IWidgetServiceCollection serviceCollection,
    IMediator mediator) :
    INotificationHandler<Created<IWidget>>
{
    public Task Handle(Created<IWidget> notification,
        CancellationToken cancellationToken)
    {
        if(notification.Value is IWidget widget)
        {
            IWidgetBuilder widgetBuilder =  widget.Create();
            if  (widgetBuilder is IWidgetServiceBuilder serviceBuilder)
            {
                serviceBuilder.ConfigureWidgetServices(serviceCollection);
            }

            widgetBuilder.Build();
        }

        return Task.CompletedTask;
    }
}
