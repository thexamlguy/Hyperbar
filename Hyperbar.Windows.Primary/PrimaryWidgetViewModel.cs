
namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IEnumerable<IWidgetComponentViewModel> items) :
    ObservableCollectionViewModel<IWidgetComponentViewModel>(serviceFactory, mediator, items),
    IWidgetViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}