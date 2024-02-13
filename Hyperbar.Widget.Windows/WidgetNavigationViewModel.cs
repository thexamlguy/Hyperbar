using Hyperbar.UI.Windows;

namespace Hyperbar.Widget.Windows;

[NotificationHandler(nameof(WidgetNavigationViewModel))]
public class WidgetNavigationViewModel(IViewModelTemplateSelector viewModelTemplateSelector,
    IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    string text) :
    NavigationViewModel<WidgetSettingsNavigationViewModel>(serviceProvider, serviceFactory, publisher, subscriber, disposer, text)
{
    public IViewModelTemplateSelector ViewModelTemplateSelector => viewModelTemplateSelector;
}
