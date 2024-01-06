using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar;

public partial class WidgetComponentViewModelBase(ITemplateFactory templateFactory) :
    ObservableObject,
    IWidgetComponentViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}
