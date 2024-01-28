using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Widget.MediaController.Windows;

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

        Add<MediaButtonViewModel>(PlaybackButtonType.Previous, 
            "Previous", "\uEB9E",
                new RelayCommand(async () => await mediator.PublishAsync<Previous>()));

        Add<MediaButtonViewModel>(PlaybackButtonType.Play, 
            "Play", "\uE768", 
                new RelayCommand(async () => await mediator.PublishAsync<Play>()));

        Add<MediaButtonViewModel>(PlaybackButtonType.Pause,
            "Pause", "\uE769", 
                new RelayCommand(async () => await mediator.PublishAsync<Pause>()));

        Add<MediaButtonViewModel>(PlaybackButtonType.Forward, 
            "Forward", "\uEB9D",
                new RelayCommand(async () => await mediator.PublishAsync<Forward>()));

        mediator.Subscribe(this);
    }

    public ITemplateFactory TemplateFactory { get; set; }
}