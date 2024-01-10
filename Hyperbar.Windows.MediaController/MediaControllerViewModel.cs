using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar.Windows.Primary;

public partial class MediaInformationViewModel :
    WidgetComponentViewModel
{
    [ObservableProperty]
    private string title = "this is a test";

    [ObservableProperty]
    private string description = "this is a test description";

    public MediaInformationViewModel(ITemplateFactory templateFactory) : base(templateFactory)
    {
    }
}

public class MediaControllerViewModel : 
    ObservableCollectionViewModel<WidgetComponentViewModel>,
    ITemplatedViewModel
{
    public MediaControllerViewModel(ITemplateFactory templateFactory,
        IServiceFactory serviceFactory,
        IMediator mediator) : base(serviceFactory, mediator)
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