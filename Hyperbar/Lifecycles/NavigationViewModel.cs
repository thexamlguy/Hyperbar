using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar;

public partial class NavigationViewModel :
    ObservableCollectionViewModel<INavigationViewModel>,
    INavigationViewModel
{
    [ObservableProperty]
    private string? text;

    public NavigationViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory, 
        IMediator mediator, 
        IDisposer disposer,
        string text) : base(serviceProvider, serviceFactory, mediator, disposer)
    {
        this.text = text;
    }
}

public partial class NavigationViewModel<TNavigationViewModel>(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    string text) :
    ObservableCollectionViewModel<TNavigationViewModel>(serviceProvider, serviceFactory, mediator, disposer),
    INavigationViewModel
    where TNavigationViewModel :
    INavigationViewModel
{
    [ObservableProperty]
    private string? text = text;
}
