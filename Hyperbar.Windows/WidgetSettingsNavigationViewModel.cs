namespace Hyperbar.Windows;

public class WidgetSettingsNavigationViewModel(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    string text) :
    NavigationViewModel<WidgetNavigationViewModel>(serviceProvider, serviceFactory, publisher, subscriber, disposer, text)
{

}
