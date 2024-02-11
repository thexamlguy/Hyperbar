namespace Hyperbar.Widget.Primary.Windows;

[NotificationHandler(nameof(PrimaryWidgetViewModel))]
public class PrimaryWidgetViewModel(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) :
    ObservableCollectionViewModel<IWidgetComponentViewModel>(serviceProvider, serviceFactory, publisher, subscriber, disposer),
    IWidgetViewModel;