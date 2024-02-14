namespace Hyperbar.Widget.Windows;

public class WidgetConfigurationNavigationViewModel(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    string text) :
    NavigationViewModel(serviceProvider, serviceFactory, publisher, subscriber, disposer, text);