using Hyperbar.UI.Windows;

namespace Hyperbar.Windows;

public class WidgetSettingsViewModel :
    ObservableCollectionViewModel<INavigationViewModel>
{
    public WidgetSettingsViewModel(IViewModelTemplateSelector viewModelTemplateSelector,
        IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        ViewModelTemplateSelector = viewModelTemplateSelector;
    }

    public IViewModelTemplateSelector ViewModelTemplateSelector { get; }
}
