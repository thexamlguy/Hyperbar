using Hyperbar.Widget;

namespace Hyperbar.Widget.Primary.Windows;

[NotificationHandler(nameof(PrimaryWidgetViewModel))]
public class PrimaryWidgetViewModel(IViewModelTemplateFactory templateFactory,
    IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer) :
    ObservableCollectionViewModel<IWidgetComponentViewModel>(serviceProvider, serviceFactory, mediator, disposer),
    IWidgetViewModel
{
    public IViewModelTemplateFactory TemplateFactory => templateFactory;
}