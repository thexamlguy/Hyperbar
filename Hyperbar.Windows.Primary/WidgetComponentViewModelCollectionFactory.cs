using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Windows.Primary;

public class WidgetComponentViewModelFactory(IServiceFactory service,
    IMediator mediator) :
    IViewModelFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?>
{
    public async ValueTask<IWidgetComponentViewModel?> CreateAsync(PrimaryCommandConfiguration value)
    {
        if (value is KeyAcceleratorCommandConfiguration keyAcceleratorCommand)
        {
            return await ValueTask.FromResult(service.Create<WidgetButtonViewModel>(keyAcceleratorCommand.Id, keyAcceleratorCommand.Icon,
                new RelayCommand(async () => await mediator.SendAsync(new KeyAcceleratorRequest((VirtualKey)
                    keyAcceleratorCommand.Key, keyAcceleratorCommand.Modifiers?
                        .Select(modifier => (VirtualKey)modifier).ToArray())))));
        }

        if (value is ProcessCommandConfiguration commandConfiguration)
        {
            return await ValueTask.FromResult(service.Create<WidgetButtonViewModel>(commandConfiguration.Id,
                commandConfiguration.Icon, new RelayCommand(async () =>
                    await mediator.SendAsync(new ProcessRequest(commandConfiguration.Path)))));
        }

        return default;
    }
}