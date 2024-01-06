using Hyperbar.Lifecycles;
using Hyperbar.Templates;
using System.Collections.Generic;

namespace Hyperbar.Windows;

public partial class CommandViewModel : 
    ObservableCollectionViewModel,
    ITemplatedViewModel
{
    public CommandViewModel(ITemplateFactory templateFactory, 
        IEnumerable<IWidgetViewModel> commands)
    {
        TemplateFactory = templateFactory;
        AddRange(commands);
    }

    public ITemplateFactory TemplateFactory { get; }
}
