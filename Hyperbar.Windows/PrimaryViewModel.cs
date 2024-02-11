using CommunityToolkit.Mvvm.ComponentModel;
using Hyperbar.UI.Windows;

namespace Hyperbar.Widget;

[NotificationHandler(nameof(IWidgetHostViewModel))]
public partial class PrimaryViewModel(IViewModelTemplateSelector viewModelTemplateSelector, 
    IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    int index) :
    ObservableCollectionViewModel<IWidgetViewModel>(serviceProvider, serviceFactory, publisher, subscriber, disposer),
    IWidgetHostViewModel
{
    [ObservableProperty]
    private int index = index;

    public IViewModelTemplateSelector ViewModelTemplateSelector => viewModelTemplateSelector;
}