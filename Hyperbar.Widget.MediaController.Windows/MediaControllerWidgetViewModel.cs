using Hyperbar.Widget;

namespace Hyperbar.Widget.MediaController.Windows;

public class MediaControllerWidgetViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    IEnumerable<MediaControllerViewModel> items) :
    ObservableCollectionViewModel<MediaControllerViewModel>(serviceFactory, mediator, disposer, items),
    IWidgetViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}