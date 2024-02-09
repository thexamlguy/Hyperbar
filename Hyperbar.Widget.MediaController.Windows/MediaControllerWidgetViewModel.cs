using Hyperbar.UI.Windows;

namespace Hyperbar.Widget.MediaController.Windows;

public class MediaControllerWidgetViewModel(IViewModelTemplate template, 
    IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    IEnumerable<MediaControllerViewModel> items) :
    ObservableCollectionViewModel<MediaControllerViewModel>(serviceProvider, serviceFactory, mediator, disposer, items),
    IWidgetViewModel
{
    public IViewModelTemplate Template => template;
}