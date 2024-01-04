namespace Hyperbar.Desktop.Contextual;

public class ContextualCommandViewModel :
    ICommandViewModel,
    ITemplatedViewModel
{
    public ContextualCommandViewModel(ITemplateFactory templateFactory)
    {
        TemplateFactory = templateFactory;
    }

    public ITemplateFactory TemplateFactory { get; }
}
