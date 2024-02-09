using Hyperbar.Widget;

namespace Hyperbar.Widget.Primary.Windows;

[NotificationHandler(nameof(PrimaryWidgetViewModel))]
public class PrimaryWidgetViewModel(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer) :
    ObservableCollectionViewModel<IWidgetComponentViewModel>(serviceProvider, serviceFactory, mediator, disposer),
    IWidgetViewModel;