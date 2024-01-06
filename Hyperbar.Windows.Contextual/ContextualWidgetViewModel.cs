using Hyperbar.Lifecycles;
using Hyperbar.Templates;

namespace Hyperbar.Extensions.Contextual;

public class ContextualWidgetViewModel(ITemplateFactory templateFactory) :
    IWidgetViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory { get; } = templateFactory;
}
