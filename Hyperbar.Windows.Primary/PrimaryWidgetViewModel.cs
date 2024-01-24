using Hyperbar.Widget;

namespace Hyperbar.Windows.Primary;

[NotificationHandler(nameof(PrimaryWidgetViewModel))]
public class PrimaryWidgetViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    IEnumerator<IWidgetComponentViewModel> enumerator) :
    ObservableCollectionViewModel<IWidgetComponentViewModel>(serviceFactory, mediator, disposer, enumerator),
    IWidgetViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}