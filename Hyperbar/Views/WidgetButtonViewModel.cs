using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hyperbar;

public partial class WidgetButtonViewModel :
    WidgetComponentViewModelBase
{
    [ObservableProperty]
    private string? icon;

    [ObservableProperty]
    private IRelayCommand? click;

    public WidgetButtonViewModel(ITemplateFactory templateFactory,
        string? icon = null,
        Action? action = null) : base(templateFactory)
    {
        this.icon = icon;
        if (action is not null)
        {
            click = new RelayCommand(action);
        }
    }
}
