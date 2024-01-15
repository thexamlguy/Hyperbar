using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Windows.MediaController;

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
        Add<MediaButtonViewModel>("Backward", "\uEB9E");
        Add<MediaButtonViewModel>("Play", "\uE768", new RelayCommand(async () => await mediator.PublishAsync<Play>()));
        Add<MediaButtonViewModel>("Pause", "\uE769", new RelayCommand(async () => await mediator.PublishAsync<Pause>()));
        Add<MediaButtonViewModel>("Forward", "\uEB9D");
    }

    public ITemplateFactory TemplateFactory { get; set; }
}