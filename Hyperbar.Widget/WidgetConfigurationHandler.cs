namespace Hyperbar.Widget;

public class WidgetConfigurationHandler(IEnumerable<IConfigurationChangedPublisher<WidgetConfiguration>> 
    configurationValueChangedNotifications) :
    INotificationHandler<Changed<WidgetConfiguration>> 
{
    public async Task Handle(Changed<WidgetConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is WidgetConfiguration configuration)
        {
            foreach (IConfigurationChangedPublisher<WidgetConfiguration> notification in 
                configurationValueChangedNotifications)
            {
                await notification.PublishAsync(configuration);
            }
        }
    }
}
