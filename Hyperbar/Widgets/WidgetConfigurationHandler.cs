namespace Hyperbar;

public class WidgetConfigurationHandler(IValue<WidgetAvailability> widgetAvailability) :
    INotificationHandler<ConfigurationChanged<WidgetConfiguration>> 
{
    public async Task Handle(ConfigurationChanged<WidgetConfiguration> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Configuration is WidgetConfiguration configuration)
        {
            await widgetAvailability.SetAsync(args => args with { Value = configuration.IsAvailable });
        }
    }
}
