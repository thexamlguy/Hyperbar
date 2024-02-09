using Hyperbar.UI.Windows;

namespace Hyperbar.Windows;

public partial class SettingsViewModel :
    ObservableCollectionViewModel<INavigationViewModel>
{
    public SettingsViewModel(IViewModelTemplate template,
        IServiceProvider serviceProvider, 
        IServiceFactory serviceFactory, 
        IMediator mediator, 
        IDisposer disposer) : base(serviceProvider, serviceFactory, mediator, disposer)
    {
        Template = template;

        Add<GeneralSettingsNavigationViewModel>("General");
        Add<WidgetSettingsNavigationViewModel>("Widgets");
    }

    public IViewModelTemplate Template { get; }
}
