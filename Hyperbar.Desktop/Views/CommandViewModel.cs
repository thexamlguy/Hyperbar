using Hyperbar.Lifecycles;
using Hyperbar.Templates;
using System.Collections;
using System.Collections.Generic;

namespace Hyperbar.Desktop;

public partial class CommandViewModel : 
    ObservableCollectionViewModel,
    ITemplatedViewModel
{
    public CommandViewModel(ITemplateFactory templateFactory, 
        IEnumerable<ICommandViewModel> commands)
    {
        TemplateFactory = templateFactory;

        foreach (var command in commands)
        {
            this.Add(command);
        }
    }

    public ITemplateFactory TemplateFactory { get; }
}
