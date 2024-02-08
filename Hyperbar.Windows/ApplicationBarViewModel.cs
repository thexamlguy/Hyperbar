namespace Hyperbar.Widget;

[NotificationHandler(nameof(IWidgetHostViewModel))]
public partial class ApplicationBarViewModel :
    ObservableCollectionViewModel<IDisposable>
{
    public ApplicationBarViewModel(IViewModelTemplateFactory templateFactory,
        IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IMediator mediator, 
        IDisposer disposer) : base(serviceProvider, serviceFactory, mediator, disposer)
    {
        TemplateFactory = templateFactory;

        Add<PrimaryViewModel>(0);
        Add<SecondaryViewModel>(1);
    }

    public IViewModelTemplateFactory TemplateFactory { get; }
}