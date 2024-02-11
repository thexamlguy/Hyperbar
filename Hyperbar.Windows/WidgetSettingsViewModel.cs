using Hyperbar.UI.Windows;

namespace Hyperbar.Windows;

public class WidgetSettingsViewModel(IViewModelTemplateSelector viewModelTemplateSelector,
    IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) :
    ObservableCollectionViewModel<IObservableViewModel>(serviceProvider, serviceFactory, publisher, subscriber, disposer)
{
    public IViewModelTemplateSelector ViewModelTemplateSelector { get; } = viewModelTemplateSelector;
}