using Hyperbar.Windows.Controls;

namespace Hyperbar.Windows;

public class AppConfigurationChangedHandler(DesktopBar desktopFlyout,
    AppConfiguration configuration) :
    INotificationHandler<ConfigurationChanged<AppConfiguration>>
{
    public Task Handle(ConfigurationChanged<AppConfiguration> notification, CancellationToken cancellationToken)
    {
        desktopFlyout.Placement = configuration.Placement;
        return Task.CompletedTask;
    }
}
