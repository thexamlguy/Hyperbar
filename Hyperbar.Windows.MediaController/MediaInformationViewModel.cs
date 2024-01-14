using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar.Windows.MediaController;

public partial class MediaInformationViewModel :
    WidgetComponentViewModel
{
    [ObservableProperty]
    private string title = "this is a test";

    [ObservableProperty]
    private string description = "this is a test description";

    public MediaInformationViewModel(IServiceFactory serviceFactory, 
        IMediator mediator, 
        IDisposer disposer, 
        ITemplateFactory templateFactory) : base(serviceFactory, mediator, disposer, templateFactory)
    {
    }
}
