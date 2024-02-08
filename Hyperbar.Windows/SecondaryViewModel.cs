using CommunityToolkit.Mvvm.ComponentModel;
using Hyperbar.Windows;

namespace Hyperbar.Widget;

public partial class SecondaryViewModel :
    ObservableCollectionViewModel<IDisposable>
{
    [ObservableProperty]
    private int index;

    public SecondaryViewModel(IViewModelTemplateFactory templateFactory,
        IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer,
        int index) : base(serviceProvider, serviceFactory, mediator, disposer)
    {       
        TemplateFactory = templateFactory;
        this.index = index;

        Add<SettingsButtonViewModel>();
    }

    public IViewModelTemplateFactory TemplateFactory { get; }
}