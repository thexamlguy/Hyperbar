using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar;

public partial class NavigationViewModel :
    ObservableCollectionViewModel<NavigationViewModel>
{
    [ObservableProperty]
    private string? text;

    public NavigationViewModel(IServiceFactory serviceFactory, 
        IMediator mediator, 
        IDisposer disposer,
        string text) : base(serviceFactory, mediator, disposer)
    {
        this.text = text;
    }
}
