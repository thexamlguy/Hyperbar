using Hyperbar.UI.Windows;

namespace Hyperbar.Windows;

public partial class SettingsButtonViewModel(IViewModelTemplateSelector viewModelTemplateSelector, 
    IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) :
    ObservableViewModel(serviceProvider, serviceFactory, publisher, subscriber, disposer)
{
    public IViewModelTemplateSelector ViewModelTemplateSelector => viewModelTemplateSelector;
}