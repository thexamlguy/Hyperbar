
namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetConfigurationHandler(IMediator mediator,
    PrimaryWidgetConfiguration configuration,
    IFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?> factory,
    ICache<Guid, IWidgetComponentViewModel> cache) :
    INotificationHandler<ConfigurationChanged<PrimaryWidgetConfiguration>>
{
    public async Task Handle(ConfigurationChanged<PrimaryWidgetConfiguration> notification,
        CancellationToken cancellationToken)
    {
        List<(Guid ParentId, Guid Id, PrimaryCommandConfiguration Configuration)> items = [];

        void AddToItems(Guid parentId, Guid id, List<PrimaryCommandConfiguration> configurations)
        {
            if (configurations is null)
            {
                return;
            }

            Stack<(Guid, List<PrimaryCommandConfiguration>)> stack = new();
            stack.Push((parentId, configurations));

            while (stack.Count > 0)
            {
                (Guid currentParentId, List<PrimaryCommandConfiguration> currentConfigurations) = stack.Pop();
                foreach (PrimaryCommandConfiguration configuration in currentConfigurations)
                {
                    items.Add((currentParentId, configuration.Id, configuration));
                    if (configuration.Commands is not null && configuration.Commands.Count > 0)
                    {
                        stack.Push((configuration.Id, configuration.Commands));
                    }
                }
            }
        }

        AddToItems(Guid.Empty, Guid.Empty, configuration.Commands);

        foreach (KeyValuePair<Guid, IWidgetComponentViewModel> item in cache
            .Where(x => !items.Any(k => x.Key == k.Id)))
        {
            await mediator.PublishAsync(new Removed<IWidgetComponentViewModel>(item.Value),
                nameof(PrimaryWidgetViewModel),
                    cancellationToken);

            cache.Remove(item.Key);
        }

        foreach ((Guid ParentId, Guid Id, PrimaryCommandConfiguration Configuration) item in 
            items.Where(x => !cache.Any(k => x.Id == k.Key)))
        {
            if (factory.Create(item.Configuration) is IWidgetComponentViewModel viewModel)
            {
                await mediator.PublishAsync(new Inserted<IWidgetComponentViewModel>(item.Configuration.Order, viewModel),
                    nameof(PrimaryWidgetViewModel),
                        cancellationToken);
            }
        }
    }
}
