using CommunityToolkit.Mvvm.ComponentModel;
using Hyperbar.UI.Windows;

namespace Hyperbar.Widget;

[NotificationHandler(nameof(IWidgetHostViewModel))]
public partial class PrimaryViewModel(IViewModelTemplate template, 
    IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    int index) :
    ObservableCollectionViewModel<IWidgetViewModel>(serviceProvider, serviceFactory, mediator, disposer),
    IWidgetHostViewModel
{
    [ObservableProperty]
    private int index = index;

    public IViewModelTemplate Template => template;

}