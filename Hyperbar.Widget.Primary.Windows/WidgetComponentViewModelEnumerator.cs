using Hyperbar.Widget;

namespace Hyperbar.Widget.Primary.Windows;

public class WidgetComponentViewModelEnumerator(PrimaryWidgetConfiguration configuration,
    IMediator mediator,
    IFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?> factory,
    ICache<(Guid ParentId, Guid Id), PrimaryCommandConfiguration> cache) :
    INotificationHandler<Enumerate<IWidgetComponentViewModel>>
{
    public async Task Handle(Enumerate<IWidgetComponentViewModel> notification, CancellationToken cancellationToken)
    {
        Stack<(Guid, List<PrimaryCommandConfiguration>)> stack = new();
        stack.Push((Guid.Empty, configuration.Commands));

        while (stack.Count > 0)
        {
            (Guid currentParentId, List<PrimaryCommandConfiguration> currentConfigurations) = stack.Pop();
            foreach (PrimaryCommandConfiguration configuration in currentConfigurations)
            {
                cache.Add((currentParentId, configuration.Id), configuration);
                if (configuration.Commands is not null && configuration.Commands.Count > 0)
                {
                    stack.Push((configuration.Id, configuration.Commands));
                }
            }
        }

        foreach (PrimaryCommandConfiguration item in configuration.Commands.OrderBy(x => x.Order))
        {
            if (factory.Create(item) is IWidgetComponentViewModel viewModel)
            {
                await mediator.PublishAsync(new Created<IWidgetComponentViewModel>(viewModel), nameof(PrimaryWidgetViewModel), 
                    cancellationToken);
            }
        }
    }
}
