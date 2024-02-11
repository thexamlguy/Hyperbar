using CommunityToolkit.Mvvm.Input;
using Hyperbar.UI.Windows;

namespace Hyperbar.Widget.MediaController.Windows;

[NotificationHandler(nameof(MediaControllerViewModel))]
public class MediaControllerViewModel : 
    ObservableCollectionViewModel<WidgetComponentViewModel>
{
    public MediaControllerViewModel(IViewModelTemplateSelector viewModelTemplateSelector,
        IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        ViewModelTemplateSelector = viewModelTemplateSelector;

        Add<MediaInformationViewModel>();

        Add<MediaButtonViewModel<MediaPreviousButton>>(new RelayCommand(async () =>
            await publisher.PublishAsync<Request<MediaPrevious>>()));

        Add<MediaButtonViewModel<MediaPlayPauseButton>>(new RelayCommand(async () =>
            await publisher.PublishAsync<Request<MediaPlayPause>>()));

        Add<MediaButtonViewModel<MediaNextButton>>(new RelayCommand(async () =>
            await publisher.PublishAsync<Request<MediaNext>>()));
    }

    public IViewModelTemplateSelector ViewModelTemplateSelector { get; }
}