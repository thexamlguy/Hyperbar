
namespace Hyperbar.Windows.Primary;

public class WidgetComponentMapping(PrimaryWidgetConfiguration configuration,
    IServiceFactory service,
    IMediator mediator) :
    IMappingHandler<PrimaryWidgetConfiguration, IEnumerable<IWidgetComponentViewModel>>
{
    public IEnumerable<IWidgetComponentViewModel> Handle()
    {
        foreach (IPrimaryCommandConfiguration item in configuration)
        {
            if (item is KeyAcceleratorCommandConfiguration keyAcceleratorCommand)
            {
                yield return service.Create<WidgetButtonViewModel>(keyAcceleratorCommand.Icon, new Action(async () =>
                   await mediator.SendAsync(new KeyAcceleratorCommand(VirtualKey.LeftWindows))));
            }
        }
    }
}