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
        Add<WidgetButtonViewModel>("Backward", "\uEB9E");
        Add<WidgetButtonViewModel>("Play", "\uE768", new RelayCommand(async () => await mediator.SendAsync(new Play())));
        Add<WidgetButtonViewModel>("Pause", "\uE769", new RelayCommand(async () => await mediator.PublishAsync(new Pause())));
        Add<WidgetButtonViewModel>("Forward", "\uEB9D");
    }

    public ITemplateFactory TemplateFactory { get; set; }
}