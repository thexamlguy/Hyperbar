namespace Hyperbar.Windows.Primary;

public class WidgetComponentMappingFactory :
    IMappingFactory<PrimaryWidgetConfiguration, IEnumerable<IWidgetComponentViewModel>>
{
    private readonly PrimaryWidgetConfiguration configuration;
    private readonly IServiceFactory service;
    private readonly IMediator mediator;

    public WidgetComponentMappingFactory(PrimaryWidgetConfiguration configuration, 
        IServiceFactory service,
        IMediator mediator)
    {
        this.configuration = configuration;
        this.service = service;
        this.mediator = mediator;
    }

    public IEnumerable<IWidgetComponentViewModel> Create()
    {
        foreach (IPrimaryCommandConfiguration item in configuration)
        {
            if (item is KeyAcceleratorCommandConfiguration keyAcceleratorCommand)
            {
                yield return service.Create<WidgetButtonViewModel>(keyAcceleratorCommand.Icon, new Action(() => 
                    mediator.Send(new KeyAcceleratorCommand(VirtualKey.LeftWindows))));
            }
        }
    }
}
