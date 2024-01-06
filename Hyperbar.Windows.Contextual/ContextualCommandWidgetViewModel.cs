using Hyperbar.Lifecycles;
using Hyperbar.Templates;

namespace Hyperbar.Windows.Contextual;

public class ContextualCommandWidgetViewModel(ITemplateFactory templateFactory) :
    ICommandWidgetViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory { get; } = templateFactory;
}
