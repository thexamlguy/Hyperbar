using Hyperbar.UI.Windows;

namespace Hyperbar.Widget.MediaController.Windows;

public class MediaControllerWidgetViewModel(IViewModelTemplate template, 
    IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IEnumerable<MediaControllerViewModel> items) :
    ObservableCollectionViewModel<MediaControllerViewModel>(serviceProvider, serviceFactory, publisher, subscriber, disposer, items),
    IWidgetViewModel
{
    public IViewModelTemplate Template => template;
}