using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Widget.MediaController.Windows;

[NotificationHandler(nameof(MediaControllerViewModel))]
public class MediaControllerViewModel : 
    ObservableCollectionViewModel<WidgetComponentViewModel>
{
    public MediaControllerViewModel(IViewModelTemplateFactory templateFactory,
        IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer) : base(serviceProvider, serviceFactory, mediator, disposer)
    {
        TemplateFactory = templateFactory;

        Add<MediaInformationViewModel>();

        Add<MediaButtonViewModel<MediaPreviousButton>>(new RelayCommand(async () => 
            await mediator.PublishAsync<Request<MediaPrevious>>()));

        Add<MediaButtonViewModel<MediaPlayPauseButton>>(new RelayCommand(async () =>
            await mediator.PublishAsync<Request<MediaPlayPause>>()));

        Add<MediaButtonViewModel<MediaNextButton>>(new RelayCommand(async () =>
            await mediator.PublishAsync<Request<MediaNext>>()));
    }

    public IViewModelTemplateFactory TemplateFactory { get; set; }
}