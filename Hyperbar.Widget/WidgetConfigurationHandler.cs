namespace Hyperbar.Widget;

public class WidgetConfigurationHandler(IEnumerable<IConfigurationValueChangedNotification<WidgetConfiguration>> 
        configurationValueChangedNotifications ) :
    INotificationHandler<Changed<WidgetConfiguration>> 
{
    public async Task Handle(Changed<WidgetConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is WidgetConfiguration configuration)
        {
            foreach (IConfigurationValueChangedNotification<WidgetConfiguration> notification in 
                configurationValueChangedNotifications)
            {
                await notification.PublishAsync(configuration);
            }
        }
    }
}
