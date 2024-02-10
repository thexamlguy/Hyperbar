using CommunityToolkit.Mvvm.ComponentModel;
using Hyperbar.UI.Windows;
using Hyperbar.Windows;

namespace Hyperbar.Widget;

public partial class SecondaryViewModel :
    ObservableCollectionViewModel<IDisposable>
{
    [ObservableProperty]
    private int index;

    public SecondaryViewModel(IViewModelTemplate template, 
        IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        int index) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        Template = template;
        this.index = index;

        Add<SettingsButtonViewModel>();
    }

    public IViewModelTemplate Template { get; }

}