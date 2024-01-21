using System.Reflection;

namespace Hyperbar;

public class WidgetAssemblyHandler(IMediator mediator,
    IFactory<Type, IWidget> factory) :
    INotificationHandler<Created<Assembly>>
{
    public Task Handle(Created<Assembly> notification, 
        CancellationToken cancellationToken)
    {
        if (notification.Value?.GetTypes().FirstOrDefault(x => typeof(IWidget).IsAssignableFrom(x)) is Type widgetType)
        {
            if (factory.Create(widgetType) is IWidget widget)
            {
                mediator.PublishAsync(new Created<IWidget>(widget), 
                    cancellationToken);
            }
        }

        return Task.CompletedTask;
    }
}
