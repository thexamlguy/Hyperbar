namespace Hyperbar.Desktop;

public class ContextualCommandViewModel :
    ITemplatedViewModel
{
    public ContextualCommandViewModel(ITemplateFactory templateFactory)
    {
        TemplateFactory = templateFactory;
    }

    public ITemplateFactory TemplateFactory { get; }
}