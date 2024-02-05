namespace Hyperbar.Windows;

public partial class SettingsViewModel :
    ObservableCollectionViewModel<NavigationViewModel>
{
    public SettingsViewModel(IServiceFactory serviceFactory, 
        IMediator mediator, 
        IDisposer disposer) : base(serviceFactory, mediator, disposer)
    {
        Add<NavigationViewModel>("General");
        Add<NavigationViewModel>("Widgets");
    }
}
