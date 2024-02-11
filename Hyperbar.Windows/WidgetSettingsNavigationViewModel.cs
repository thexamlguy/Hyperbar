using Hyperbar.UI.Windows;

namespace Hyperbar.Windows;

[NotificationHandler(nameof(WidgetSettingsNavigationViewModel))]
public class WidgetSettingsNavigationViewModel(IViewModelTemplateSelector viewModelTemplateSelector,
    IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    string text) :
    NavigationViewModel<WidgetNavigationViewModel>(serviceProvider, serviceFactory, publisher, subscriber, disposer, text)
{
    public IViewModelTemplateSelector ViewModelTemplateSelector => viewModelTemplateSelector;
}
