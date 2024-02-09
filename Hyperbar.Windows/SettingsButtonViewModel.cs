using Hyperbar.UI.Windows;

namespace Hyperbar.Windows;

public partial class SettingsButtonViewModel(IViewModelTemplate template, 
    IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer) :
    ObservableViewModel(serviceProvider, serviceFactory, mediator, disposer)
{
    public IViewModelTemplate Template => template;
}