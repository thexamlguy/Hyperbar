using CommunityToolkit.Mvvm.Input;
using Hyperbar.UI.Windows;

namespace Hyperbar.Widget.MediaController.Windows;

[NotificationHandler(nameof(MediaControllerViewModel))]
public class MediaControllerViewModel : 
    ObservableCollectionViewModel<WidgetComponentViewModel>
{
    public MediaControllerViewModel(IViewModelTemplate template,
        IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer) : base(serviceProvider, serviceFactory, mediator, disposer)
    {
        Template = template;

        Add<MediaInformationViewModel>();

        Add<MediaButtonViewModel<MediaPreviousButton>>(new RelayCommand(async () =>
            await mediator.PublishAsync<Request<MediaPrevious>>()));

        Add<MediaButtonViewModel<MediaPlayPauseButton>>(new RelayCommand(async () =>
            await mediator.PublishAsync<Request<MediaPlayPause>>()));

        Add<MediaButtonViewModel<MediaNextButton>>(new RelayCommand(async () =>
            await mediator.PublishAsync<Request<MediaNext>>()));
    }

    public IViewModelTemplate Template { get; }
}