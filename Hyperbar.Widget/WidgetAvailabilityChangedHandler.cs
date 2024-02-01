namespace Hyperbar.Widget;

public class WidgetAvailabilityChangedHandler(IWidgetHost host) : 
    INotificationHandler<Changed<WidgetAvailability>>
{
    public async Task Handle(Changed<WidgetAvailability> args, 
        CancellationToken cancellationToken)
    {
        if (args.Value is WidgetAvailability widgetAvailability)
        {
            if (widgetAvailability.Value)
            {
                await host.StartAsync(cancellationToken);
            }
            else
            {
                await host.StopAsync(cancellationToken);
            }
        }
    }
}
