using CommunityToolkit.Mvvm.Input;
using Hyperbar.Widget;

namespace Hyperbar.Widget.Primary.Windows;

public class WidgetComponentFactory(IServiceFactory factory,
    IMediator mediator,
    ICache<Guid, IWidgetComponentViewModel> cache) :
    IFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?>
{
    public IWidgetComponentViewModel? Create(PrimaryCommandConfiguration configuration)
    {
        WidgetComponentViewModel? viewModel = default;

        if (configuration is KeyAcceleratorCommandConfiguration keyAcceleratorCommandConfiguration)
        {
            viewModel = factory.Create<WidgetButtonViewModel>(keyAcceleratorCommandConfiguration.Id,
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
                        childViewModel = factory.Create<WidgetMenuViewModel>(childProcessCommandConfiguration.Id,
                              childProcessCommandConfiguration.Icon, childProcessCommandConfiguration.Text,
                              new RelayCommand(async () => await mediator.SendAsync(new StartProcess(childProcessCommandConfiguration.Path))));
                    }

                    if (childCommandConfiguration is KeyAcceleratorCommandConfiguration childKeyAcceleratorCommandConfiguration)
                    {
                        childViewModel = factory.Create<WidgetMenuViewModel>(childKeyAcceleratorCommandConfiguration.Id,
                            childKeyAcceleratorCommandConfiguration.Text, childKeyAcceleratorCommandConfiguration.Icon, 
                            new RelayCommand(async () => 
                                await mediator.SendAsync(new KeyAccelerator((VirtualKey)childKeyAcceleratorCommandConfiguration.Key,
                            childKeyAcceleratorCommandConfiguration.Modifiers?.Select(modifier => (VirtualKey)modifier).ToArray()))));
                    }

                    if (childViewModel is not null)
                    {
                        childViewModels.Add(childViewModel);
                        cache.Add(childCommandConfiguration.Id, childViewModel);
                    }
                }

                viewModel = factory.Create<WidgetSplitButtonViewModel>(childViewModels, 
                    processCommandConfiguration.Id, processCommandConfiguration.Text, 
                    processCommandConfiguration.Icon, new RelayCommand(async () =>
                        await mediator.SendAsync(new StartProcess(processCommandConfiguration.Path))));
            }
            else
            {
                viewModel = factory.Create<WidgetButtonViewModel>(processCommandConfiguration.Id,
                    processCommandConfiguration.Text, processCommandConfiguration.Icon, new RelayCommand(async () =>
                        await mediator.SendAsync(new StartProcess(processCommandConfiguration.Path))));
            }
        }

        if (viewModel is not null)
        {
            cache.Add(configuration.Id, viewModel);
        }

        return viewModel;
    }
}