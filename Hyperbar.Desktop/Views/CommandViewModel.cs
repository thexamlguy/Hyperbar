namespace Hyperbar.Desktop;

public partial class CommandViewModel : 
    ObservableCollectionViewModel,
    ITemplatedViewModel
{
    public CommandViewModel(ITemplateFactory templateFactory)
    {
        TemplateFactory = templateFactory;

        this.Add(new ContextualCommandViewModel(templateFactory));
        this.Add(new ContextualCommandViewModel(templateFactory));
        this.Add(new ContextualCommandViewModel(templateFactory));
        this.Add(new ContextualCommandViewModel(templateFactory));
        this.Add(new ContextualCommandViewModel(templateFactory));

        var d = Items;
    }

    public ITemplateFactory TemplateFactory { get; }
}
