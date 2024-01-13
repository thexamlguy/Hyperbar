using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar;

public partial class WidgetComponentViewModel :
    ObservableViewModel,
    IWidgetComponentViewModel,
    ITemplatedViewModel,
    INotificationHandler<Removed<IWidgetComponentViewModel>>
{
    private readonly IMediator mediator;
    private readonly IServiceFactory serviceFactory;

    [ObservableProperty]
    private Guid id;

    public WidgetComponentViewModel(IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer,
        ITemplateFactory templateFactory,
        Guid id = default) : base(serviceFactory, mediator, disposer)
    {
        this.serviceFactory = serviceFactory;
        this.mediator = mediator;
        this.id = id;

        TemplateFactory = templateFactory;

        mediator.Subscribe(this);
    }

    public ITemplateFactory TemplateFactory { get; private set; }

    public ValueTask Handle(Removed<IWidgetComponentViewModel> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Value.Equals(this))
        {
            Dispose();
        }

        return ValueTask.CompletedTask;
    }
}