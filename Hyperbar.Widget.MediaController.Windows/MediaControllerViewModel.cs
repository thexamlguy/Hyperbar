using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Widget.MediaController.Windows;



[NotificationHandler(nameof(MediaControllerViewModel))]
public class MediaControllerViewModel : 
    ObservableCollectionViewModel<WidgetComponentViewModel>,
    ITemplatedViewModel
{
    public MediaControllerViewModel(ITemplateFactory templateFactory,
        IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer) : base(serviceFactory, mediator, disposer)
    {
        TemplateFactory = templateFactory;

        Add<MediaInformationViewModel>();

        Add<MediaButtonViewModel<MediaPreviousButton>>(new RelayCommand(async () => 
            await mediator.PublishAsync<Request<MediaPrevious>>()));

        Add<MediaButtonViewModel<MediaPlayPauseButton>>(new RelayCommand(async () =>
            await mediator.PublishAsync<Request<MediaPlayPauseButton>>()));

        Add<MediaButtonViewModel<MediaNextButton>>(new RelayCommand(async () =>
            await mediator.PublishAsync<Request<MediaNext>>()));
    }

    public ITemplateFactory TemplateFactory { get; set; }
}