namespace Hyperbar;

public class WidgetConfigurationHandler :
    INotificationHandler<ConfigurationChanged<WidgetConfiguration>> 
{
    public Task Handle(ConfigurationChanged<WidgetConfiguration> notification, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
