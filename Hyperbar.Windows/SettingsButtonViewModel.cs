using Hyperbar.UI.Windows;

namespace Hyperbar.Windows;

public partial class SettingsButtonViewModel(IViewModelTemplate template, 
    IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    IDisposer disposer) :
    ObservableViewModel(serviceProvider, serviceFactory, publisher, disposer)
{
    public IViewModelTemplate Template => template;
}