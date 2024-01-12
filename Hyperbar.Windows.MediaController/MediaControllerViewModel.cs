using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Windows.Primary;

//public class MediaControllerViewModelFactory(IServiceFactory service,
//    IMediator mediator,
//    Queue<MediaController> mediaControllers) :
//    IFactory<MediaControllerViewModel>
//{
//    public MediaControllerViewModel Create()
//    {
//        throw new NotImplementedException();
//    }
//}

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

        this.Add<MediaInformationViewModel>();
        this.Add<WidgetButtonViewModel>("\uEB9E");
        this.Add<WidgetButtonViewModel>("\uE768");
        this.Add<WidgetButtonViewModel>("\uE769");
        this.Add<WidgetButtonViewModel>("\uEB9D");
    }

    public ITemplateFactory TemplateFactory { get; set; }
}