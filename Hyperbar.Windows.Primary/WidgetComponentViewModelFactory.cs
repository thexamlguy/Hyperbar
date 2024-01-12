using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Windows.Primary;

public class WidgetComponentViewModelFactory(IServiceFactory service,
    IMediator mediator,
    IViewModelCache<Guid, IWidgetComponentViewModel> cache) :
    IViewModelFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?>
{
    public async ValueTask<IWidgetComponentViewModel?> CreateAsync(PrimaryCommandConfiguration value)
    {
        IWidgetComponentViewModel? viewModel = null;

        if (value is KeyAcceleratorCommandConfiguration keyAcceleratorCommand)
        {
            viewModel = service.Create<WidgetButtonViewModel>(keyAcceleratorCommand.Id, keyAcceleratorCommand.Icon,
                new RelayCommand(async () => await mediator.SendAsync(new KeyAcceleratorRequest((VirtualKey)
                    keyAcceleratorCommand.Key, keyAcceleratorCommand.Modifiers?
                        .Select(modifier => (VirtualKey)modifier).ToArray()))));
        }

        if (value is ProcessCommandConfiguration commandConfiguration)
        {
            viewModel = service.Create<WidgetButtonViewModel>(commandConfiguration.Id,
                commandConfiguration.Icon, new RelayCommand(async () =>
                    await mediator.SendAsync(new ProcessRequest(commandConfiguration.Path))));
        }

        if (viewModel is not null)
        {
            cache.Add(value.Id, viewModel);
        }

        return viewModel ?? default;
    }
}