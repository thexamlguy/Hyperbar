namespace Hyperbar;

public class WidgetViewModelBase(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory) :
    ObservableCollectionViewModel<IWidgetComponentViewModel>(serviceFactory),
    IWidgetViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}
