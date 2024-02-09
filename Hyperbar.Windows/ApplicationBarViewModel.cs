using Hyperbar.UI.Windows;

namespace Hyperbar.Widget;


[NotificationHandler(nameof(IWidgetHostViewModel))]
public partial class ApplicationBarViewModel :
    ObservableCollectionViewModel<IDisposable>
{
    public ApplicationBarViewModel(IViewModelTemplate template, 
        IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IMediator mediator, 
        IDisposer disposer) : base(serviceProvider, serviceFactory, mediator, disposer)
    {
        Template = template;

        Add<PrimaryViewModel>(0);
        Add<SecondaryViewModel>(1);
    }

    public IViewModelTemplate Template { get; }
}