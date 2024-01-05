using Hyperbar.Lifecycles;
using Hyperbar.Templates;
using System.Collections.Generic;

namespace Hyperbar.Desktop;

public partial class CommandViewModel : 
    ObservableCollectionViewModel,
    ITemplatedViewModel
{
    public CommandViewModel(ITemplateFactory templateFactory, 
        IEnumerable<ICommandViewModel> commands,
        IWritableConfiguration<AppConfiguration> options)
    {
        TemplateFactory = templateFactory;
        AddRange(commands);

        options.Update(args => { });
    }

    public ITemplateFactory TemplateFactory { get; }
}
