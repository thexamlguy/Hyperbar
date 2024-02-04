namespace Hyperbar.Widget;

[NotificationHandler(nameof(IWidgetHostViewModel))]
public partial class ApplicationBarViewModel :
    ObservableCollectionViewModel<IDisposable>,
    ITemplatedViewModel
{
    public ApplicationBarViewModel(ITemplateFactory templateFactory,
        IServiceFactory serviceFactory,
        IMediator mediator, 
        IDisposer disposer) : base(serviceFactory, mediator, disposer)
    {
        TemplateFactory = templateFactory;

        Add<PrimaryViewModel>(0);
        Add<SecondaryViewModel>(1);
    }

    public ITemplateFactory TemplateFactory { get; }
}