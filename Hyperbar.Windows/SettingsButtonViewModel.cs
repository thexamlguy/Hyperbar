using Hyperbar.UI.Windows;

namespace Hyperbar.Windows;

public partial class SettingsButtonViewModel(IViewModelTemplateSelector viewModelTemplateSelector, 
    IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    IDisposer disposer) :
    ObservableViewModel(serviceProvider, serviceFactory, publisher, disposer)
{
    public IViewModelTemplateSelector ViewModelTemplateSelector => viewModelTemplateSelector;
}