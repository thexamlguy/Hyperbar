using CommunityToolkit.Mvvm.ComponentModel;
using Hyperbar.UI.Windows;
using Hyperbar.Windows;

namespace Hyperbar.Widget;

public partial class SecondaryViewModel :
    ObservableCollectionViewModel<IDisposable>
{
    [ObservableProperty]
    private int index;

    public SecondaryViewModel(IViewModelTemplateSelector viewModelTemplateSelector, 
        IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        int index) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        ViewModelTemplateSelector = viewModelTemplateSelector;
        this.index = index;

        Add<SettingsButtonViewModel>();
    }

    public IViewModelTemplateSelector ViewModelTemplateSelector { get; }

}