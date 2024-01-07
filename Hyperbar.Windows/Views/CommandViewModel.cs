
namespace Hyperbar.Windows;

public partial class CommandViewModel :
    ObservableCollectionViewModel<IWidgetViewModel>,
    ITemplatedViewModel
{
    public CommandViewModel(ITemplateFactory templateFactory, 
        IServiceFactory serviceFactory,
        IMediator mediator,
        IEnumerable<IWidgetViewModel> items) : base(serviceFactory, mediator, items)
    {
        TemplateFactory = templateFactory;
    }

    public ITemplateFactory TemplateFactory { get; }
}