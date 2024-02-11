using System.Security.Policy;

namespace Hyperbar.Windows;

public class WidgetNavigationViewModelEnumerator(IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<Enumerate<WidgetNavigationViewModel>>
{
    public async Task Handle(Enumerate<WidgetNavigationViewModel> args,
        CancellationToken cancellationToken = default)
    {
        await publisher.PublishAsync(new Create<WidgetNavigationViewModel>(serviceFactory
            .Create<WidgetNavigationViewModel>("fo")), nameof(WidgetSettingsNavigationViewModel), cancellationToken);
    }
}
