using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar.Widget;

public partial class SecondaryViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    int index) :
    ObservableCollectionViewModel<IDisposable>(serviceFactory, mediator, disposer),
    ITemplatedViewModel
{
    [ObservableProperty]
    private int index = index;

    public ITemplateFactory TemplateFactory => templateFactory;
}