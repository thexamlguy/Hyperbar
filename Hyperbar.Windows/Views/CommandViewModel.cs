namespace Hyperbar.Windows;

public partial class CommandViewModel : 
    ObservableCollectionViewModel<IWidgetViewModel>,
    ITemplatedViewModel
{
    public CommandViewModel(ITemplateFactory templateFactory,
        IServiceFactory serviceFactory,
        IEnumerable<IWidgetViewModel> widgets) : base(serviceFactory)
    {
        TemplateFactory = templateFactory;
        AddRange(widgets);
    }

    public ITemplateFactory TemplateFactory { get; }
}
