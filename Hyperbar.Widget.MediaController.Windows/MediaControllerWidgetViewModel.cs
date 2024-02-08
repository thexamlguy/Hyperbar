namespace Hyperbar.Widget.MediaController.Windows;

public class MediaControllerWidgetViewModel(IViewModelTemplateFactory templateFactory,
    IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    IEnumerable<MediaControllerViewModel> items) :
    ObservableCollectionViewModel<MediaControllerViewModel>(serviceProvider, serviceFactory, mediator, disposer, items),
    IWidgetViewModel
{
    public IViewModelTemplateFactory TemplateFactory => templateFactory;
}