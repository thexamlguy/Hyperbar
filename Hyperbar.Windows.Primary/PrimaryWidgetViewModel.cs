
namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    IViewModelEnumerator<IWidgetComponentViewModel> enumerator) :
    ObservableCollectionViewModel<IWidgetComponentViewModel>(serviceFactory, mediator, disposer, enumerator),
    IWidgetViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}