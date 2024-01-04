namespace Hyperbar.Desktop.Primary;

public class PrimaryCommandViewModel(ITemplateFactory templateFactory) :
    ICommandViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory { get; } = templateFactory;
}

