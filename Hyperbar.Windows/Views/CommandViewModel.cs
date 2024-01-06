
namespace Hyperbar.Windows;

public partial class CommandViewModel :
    ObservableCollectionViewModel<IWidgetViewModel>,
    ITemplatedViewModel
{
    public CommandViewModel(ITemplateFactory templateFactory, 
        IServiceFactory serviceFactory, 
        IEnumerable<IWidgetViewModel> items) : base(serviceFactory, items)
    {
        TemplateFactory = templateFactory;
    }

    public ITemplateFactory TemplateFactory { get; }
}