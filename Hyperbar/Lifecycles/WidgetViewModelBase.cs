using Hyperbar.Templates;

namespace Hyperbar.Lifecycles;

public class WidgetViewModelBase(ITemplateFactory templateFactory) :
    ObservableCollectionViewModel<IWidgetComponentViewModel>,
    IWidgetViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory { get; } = templateFactory;
}
