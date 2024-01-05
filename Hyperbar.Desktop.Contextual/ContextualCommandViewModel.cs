using Hyperbar.Lifecycles;
using Hyperbar.Templates;

namespace Hyperbar.Desktop.Contextual;

public class ContextualCommandViewModel(ITemplateFactory templateFactory) :
    ICommandViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory { get; } = templateFactory;
}
