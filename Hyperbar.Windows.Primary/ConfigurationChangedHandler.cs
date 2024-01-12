namespace Hyperbar.Windows.Primary;

public class ConfigurationChangedHandler :
    INotificationHandler<ConfigurationChanged<PrimaryWidgetConfiguration>>
{
    public ValueTask Handle(ConfigurationChanged<PrimaryWidgetConfiguration> notification, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
