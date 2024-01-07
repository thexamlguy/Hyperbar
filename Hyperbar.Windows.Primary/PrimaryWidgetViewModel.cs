
namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetViewModel :
    ObservableCollectionViewModel<IWidgetComponentViewModel>,
    IWidgetViewModel,
    ITemplatedViewModel
{
    public PrimaryWidgetViewModel(ITemplateFactory templateFactory, 
        IServiceFactory serviceFactory,
        IMediator mediator, 
        IEnumerable<IWidgetComponentViewModel> items) : base(serviceFactory, mediator, items)
    {
        TemplateFactory = templateFactory;
    }

    public ITemplateFactory TemplateFactory { get; }
}