using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Windows.Primary;

public class WidgetComponentViewModelFactory(IServiceFactory service,
    IMediator mediator,
    ICache<Guid, IWidgetComponentViewModel> cache) :
    IFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?>
{
    public IWidgetComponentViewModel? Create(PrimaryCommandConfiguration configuration)
    {
        WidgetComponentViewModel? viewModel = default;

        if (configuration is KeyAcceleratorCommandConfiguration keyAcceleratorCommandConfiguration)
        {
            viewModel = service.Create<WidgetButtonViewModel>(keyAcceleratorCommandConfiguration.Id,
                keyAcceleratorCommandConfiguration.Text, keyAcceleratorCommandConfiguration.Icon,
                new RelayCommand(async () => await mediator.SendAsync(new KeyAccelerator((VirtualKey)
                    keyAcceleratorCommandConfiguration.Key, keyAcceleratorCommandConfiguration.Modifiers?
                        .Select(modifier => (VirtualKey)modifier).ToArray()))));
        }
        
        if (configuration is ProcessCommandConfiguration processCommandConfiguration)
        {
            if (processCommandConfiguration.Commands is { Count: > 0 } childCommandConfigurations)
            {
                List<IWidgetComponentViewModel> childViewModels = [];
                foreach (PrimaryCommandConfiguration childCommandConfiguration in childCommandConfigurations)
                {
                    WidgetComponentViewModel? childViewModel = null;

                    if (childCommandConfiguration is ProcessCommandConfiguration childProcessCommandConfiguration)
                    {
                        childViewModel = service.Create<WidgetMenuViewModel>(childProcessCommandConfiguration.Id,
                              childProcessCommandConfiguration.Icon, childProcessCommandConfiguration.Text,
                              new RelayCommand(async () => await mediator.SendAsync(new StartProcess(childProcessCommandConfiguration.Path))));
                    }

                    if (childCommandConfiguration is KeyAcceleratorCommandConfiguration childKeyAcceleratorCommandConfiguration)
                    {
                        childViewModel = service.Create<WidgetMenuViewModel>(childKeyAcceleratorCommandConfiguration.Id,
                            childKeyAcceleratorCommandConfiguration.Text, childKeyAcceleratorCommandConfiguration.Icon, 
                            new RelayCommand(async () => 
                                await mediator.SendAsync(new KeyAccelerator((VirtualKey)childKeyAcceleratorCommandConfiguration.Key,
                            childKeyAcceleratorCommandConfiguration.Modifiers?.Select(modifier => (VirtualKey)modifier).ToArray()))));
                    }

                    if (childViewModel is not null)
                    {
                        childViewModels.Add(childViewModel);
                        cache.Add(childViewModel.Id, childViewModel);
                    }
                }

                viewModel = service.Create<WidgetSplitButtonViewModel>(childViewModels, 
                    processCommandConfiguration.Id, processCommandConfiguration.Text, 
                    processCommandConfiguration.Icon, new RelayCommand(async () =>
                        await mediator.SendAsync(new StartProcess(processCommandConfiguration.Path))));
            }
            else
            {
                viewModel = service.Create<WidgetButtonViewModel>(processCommandConfiguration.Id,
                    processCommandConfiguration.Text, processCommandConfiguration.Icon, new RelayCommand(async () =>
                        await mediator.SendAsync(new StartProcess(processCommandConfiguration.Path))));
            }
        }

        if (viewModel is not null)
        {
            cache.Add(viewModel.Id, viewModel);
        }

        return viewModel;
    }
}