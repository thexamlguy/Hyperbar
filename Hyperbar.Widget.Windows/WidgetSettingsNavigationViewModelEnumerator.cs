using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget.Windows;

public class WidgetSettingsNavigationViewModelEnumerator(IPublisher publisher,
    IWidgetHostCollection widgetHosts) :
    INotificationHandler<Enumerate<WidgetSettingsNavigationViewModel>>
{
    public async Task Handle(Enumerate<WidgetSettingsNavigationViewModel> args,
        CancellationToken cancellationToken = default)
    {
        foreach (IWidgetHost host in widgetHosts)
        {
            if (host.Services.GetService<IServiceFactory>() is IServiceFactory serviceFactory)
            {
                await publisher.PublishAsync(new Create<WidgetSettingsNavigationViewModel>(serviceFactory
                    .Create<WidgetSettingsNavigationViewModel>(host.Configuration.Name)),
                        nameof(WidgetNavigationViewModel), cancellationToken);
            }
        }
    }
}
