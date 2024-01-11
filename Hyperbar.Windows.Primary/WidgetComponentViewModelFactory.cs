namespace Hyperbar.Windows.Primary;

public class WidgetComponentViewModelFactory(PrimaryWidgetConfiguration configuration,
    IServiceFactory service,
    IMediator mediator) : 
    IFactory<IEnumerable<IWidgetComponentViewModel>>
{
    public IEnumerable<IWidgetComponentViewModel> Create()
    {
        foreach (PrimaryCommandConfiguration item in configuration)
        {
            if (item is KeyAcceleratorCommandConfiguration keyAcceleratorCommandConfiguration)
            {
                yield return service.Create<WidgetButtonViewModel>(keyAcceleratorCommandConfiguration.Icon, new Action(async () =>
                  await mediator.SendAsync(new KeyAcceleratorRequest((VirtualKey)keyAcceleratorCommandConfiguration.Key,
                   keyAcceleratorCommandConfiguration.Modifiers?.Select(modifier => (VirtualKey)modifier).ToArray()))));
            }

            if (item is ProcessCommandConfiguration processCommandConfiguration)
            {
                yield return service.Create<WidgetButtonViewModel>(processCommandConfiguration.Icon, new Action(async () =>
                   await mediator.SendAsync(new ProcessRequest(processCommandConfiguration.Path))));
            }
        }
    }
}