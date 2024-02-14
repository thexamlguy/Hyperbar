using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget.Windows;

public class WidgetConfigurationNavigationViewModelEnumerator(IPublisher publisher,
    IWidgetHostCollection widgetHosts) :
    INotificationHandler<Enumerate<WidgetConfigurationNavigationViewModel>>
{
    public async Task Handle(Enumerate<WidgetConfigurationNavigationViewModel> args,
        CancellationToken cancellationToken = default)
    {
        foreach (IWidgetHost host in widgetHosts)
        {
            if (host.Services.GetService<IServiceFactory>() is IServiceFactory serviceFactory)
            {
                await publisher.PublishAsync(new Create<WidgetConfigurationNavigationViewModel>(serviceFactory
                    .Create<WidgetConfigurationNavigationViewModel>(host.Configuration.Name)),
                        nameof(WidgetNavigationViewModel), cancellationToken);
            }
        }
    }
}
