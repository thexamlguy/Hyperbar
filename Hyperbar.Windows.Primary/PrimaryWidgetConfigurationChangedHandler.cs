namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetConfigurationChangedHandler :
    INotificationHandler<ConfigurationChanged<PrimaryWidgetConfiguration>>
{
    private readonly IMediator mediator;
    private readonly IEnumerable<IWidgetComponentViewModel> items;

    public PrimaryWidgetConfigurationChangedHandler(IMediator mediator,
        IEnumerable<IWidgetComponentViewModel> items)
    {
        this.mediator = mediator;
        this.items = items;
    }

    public async ValueTask Handle(ConfigurationChanged<PrimaryWidgetConfiguration> notification, 
        CancellationToken cancellationToken)
    {
        await mediator.PublishAsync(new CollectionChanged<IEnumerable<IWidgetComponentViewModel>>(items), 
            cancellationToken);
    }
}
