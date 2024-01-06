namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetViewModel :
    ObservableCollectionViewModel<IWidgetComponentViewModel>,
    IWidgetViewModel,
    ITemplatedViewModel
{
    public PrimaryWidgetViewModel(ITemplateFactory templateFactory, 
        IServiceFactory serviceFactory,
        IEnumerable<IWidgetComponentViewModel> items) : base(serviceFactory, items)
    {
        TemplateFactory = templateFactory;
    }

    public ITemplateFactory TemplateFactory { get; }
}