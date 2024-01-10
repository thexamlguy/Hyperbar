using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar;

public partial class WidgetComponentViewModel(ITemplateFactory templateFactory) :
    ObservableObject,
    IWidgetComponentViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}