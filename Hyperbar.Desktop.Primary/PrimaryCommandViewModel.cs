using Hyperbar.Lifecycles;
using Hyperbar.Templates;

namespace Hyperbar.Desktop.Primary;

public class PrimaryCommandConfiguration
{

}

public class PrimaryCommandViewModel(ITemplateFactory templateFactory) :
    ICommandViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory { get; } = templateFactory;
}

