
namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IFactory<IEnumerable<IWidgetComponentViewModel>> factory) :
    ObservableCollectionViewModel<IWidgetComponentViewModel>(serviceFactory, mediator, factory),
    IWidgetViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}