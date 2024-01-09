namespace Hyperbar.Windows.Primary;

public class WidgetComponentMapping(PrimaryWidgetConfiguration configuration,
    IServiceFactory service,
    IMediator mediator) :
    IMappingHandler<PrimaryWidgetConfiguration, IEnumerable<IWidgetComponentViewModel>>
{
    public IEnumerable<IWidgetComponentViewModel> Handle()
    {
        foreach (var item in configuration)
        {
            if (item is KeyAcceleratorCommandConfiguration keyAcceleratorCommandConfiguration)
            {
                 yield return service.Create<WidgetButtonViewModel>(keyAcceleratorCommandConfiguration.Icon, new Action(async () =>
                   await mediator.SendAsync(new KeyAcceleratorRequest((VirtualKey)keyAcceleratorCommandConfiguration.Key, 
                    keyAcceleratorCommandConfiguration.Modifiers?.Select(modifier => (VirtualKey)modifier).ToArray()))));
            }

            if (item is ProcessCommandConfiguration processCommandConfiguration)
            {

            }
        }
    }
}