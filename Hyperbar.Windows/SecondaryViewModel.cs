using CommunityToolkit.Mvvm.ComponentModel;
using Hyperbar.Windows;

namespace Hyperbar.Widget;

public partial class SecondaryViewModel :
    ObservableCollectionViewModel<IDisposable>,
    ITemplatedViewModel
{
    [ObservableProperty]
    private int index;

    public SecondaryViewModel(ITemplateFactory templateFactory,
        IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer,
        int index) : base(serviceFactory, mediator, disposer)
    {
        this.index = index;
        this.TemplateFactory = templateFactory;

        Add<SettingsButtonViewModel>();
    }

    public ITemplateFactory TemplateFactory { get; }
}