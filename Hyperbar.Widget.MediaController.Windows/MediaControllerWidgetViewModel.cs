using Hyperbar.UI.Windows;

namespace Hyperbar.Widget.MediaController.Windows;

public class MediaControllerWidgetViewModel(IViewModelTemplateSelector viewModelTemplateSelector, 
    IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IEnumerable<MediaControllerViewModel> items) :
    ObservableCollectionViewModel<MediaControllerViewModel>(serviceProvider, serviceFactory, publisher, subscriber, disposer, items),
    IWidgetViewModel
{
    public IViewModelTemplateSelector ViewModelTemplateSelector => viewModelTemplateSelector;
}