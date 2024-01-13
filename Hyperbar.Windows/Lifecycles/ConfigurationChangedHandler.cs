using Hyperbar.Windows.Controls;

namespace Hyperbar.Windows.Primary;

public class AppConfigurationChangedHandler(DesktopFlyout desktopFlyout,
    AppConfiguration configuration) :
    INotificationHandler<ConfigurationChanged<AppConfiguration>>
{
    public ValueTask Handle(ConfigurationChanged<AppConfiguration> notification, CancellationToken cancellationToken)
    {
        desktopFlyout.Placement = configuration.Placement;
        return ValueTask.CompletedTask;
    }
}
