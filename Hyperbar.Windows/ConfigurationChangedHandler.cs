using Hyperbar.Controls.Windows;

namespace Hyperbar.Windows;

public class AppConfigurationChangedHandler(DesktopApplicationBar desktopFlyout,
    AppConfiguration configuration) :
    INotificationHandler<Changed<AppConfiguration>>
{
    public Task Handle(Changed<AppConfiguration> notification, CancellationToken cancellationToken)
    {
        desktopFlyout.Placement = configuration.Placement;
        return Task.CompletedTask;
    }
}
