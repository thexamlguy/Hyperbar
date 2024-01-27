using Hyperbar.Widget;

namespace Hyperbar.Widget.Primary.Windows;

public class PrimaryWidgetConfigurationHandler(IMediator mediator,
    PrimaryWidgetConfiguration configuration,
    IFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?> factory,
    IProvider<PrimaryCommandConfiguration, IWidgetComponentViewModel?> provider,
    ICache<(Guid ParentId, Guid Id), PrimaryCommandConfiguration> cache) :
    INotificationHandler<ConfigurationChanged<PrimaryWidgetConfiguration>>
{
    public async Task Handle(ConfigurationChanged<PrimaryWidgetConfiguration> notification,
        CancellationToken cancellationToken)
    {
        List<KeyValuePair<(Guid ParentId, Guid Id), PrimaryCommandConfiguration>> items = [];

        Stack<(Guid, List<PrimaryCommandConfiguration>)> stack = new();
        stack.Push((Guid.Empty, configuration.Commands));

        while (stack.Count > 0)
        {
            (Guid currentParentId, List<PrimaryCommandConfiguration> currentConfigurations) = stack.Pop();
            foreach (PrimaryCommandConfiguration configuration in currentConfigurations)
            {
                var key = (currentParentId, configuration.Id);
                items.Add(new KeyValuePair<(Guid ParentId, Guid Id), PrimaryCommandConfiguration>(key, configuration));

                if (configuration.Commands?.Count > 0)
                {
                    stack.Push((configuration.Id, configuration.Commands));
                }
            }
        }

        HashSet<Guid> cacheIds = new(cache.Select(x => x.Key.Id));
        HashSet<Guid> itemIds = new(items.Select(x => x.Key.Id));

        List<KeyValuePair<(Guid ParentId, Guid Id), PrimaryCommandConfiguration>> movedItems =
            items.ExceptBy(cache.Select(x => new { x.Value.Order, x.Value.Id }), x =>
                new { x.Value.Order, x.Value.Id }).ToList();

        foreach (KeyValuePair<(Guid ParentId, Guid Id), PrimaryCommandConfiguration> moved in movedItems)
        {
            if (moved.Value is PrimaryCommandConfiguration configuration &&
                provider.Get(configuration) is IWidgetComponentViewModel viewModel)
            {
                await mediator.PublishAsync(new Moved<IWidgetComponentViewModel>(configuration.Order, viewModel),
                    moved.Key.ParentId == Guid.Empty ? nameof(PrimaryWidgetViewModel) : moved.Key.ParentId,
                        cancellationToken);

                cache.Remove(moved.Key);
                cache.Add(moved.Key, moved.Value);
            }
        }

        List<KeyValuePair<(Guid ParentId, Guid Id), PrimaryCommandConfiguration>> addedItems = 
            items.ExceptBy(cacheIds.Select(x => x), x => x.Key.Id).ToList();

        foreach (KeyValuePair<(Guid ParentId, Guid Id), PrimaryCommandConfiguration> added in addedItems)
        {
            if (added.Value is PrimaryCommandConfiguration configuration &&
                factory.Create(configuration) is IWidgetComponentViewModel viewModel)
            {
                await mediator.PublishAsync(
                    new Inserted<IWidgetComponentViewModel>(configuration.Order, viewModel),
                    added.Key.ParentId == Guid.Empty ? nameof(PrimaryWidgetViewModel) : added.Key.ParentId,
                    cancellationToken);

                cache.Add(added.Key, added.Value);
            }
        }

        List<KeyValuePair<(Guid ParentId, Guid Id), PrimaryCommandConfiguration>> removedItems =
            cache.ExceptBy(itemIds.Select(x => x), x => x.Key.Id).ToList();

        foreach (KeyValuePair<(Guid ParentId, Guid Id), PrimaryCommandConfiguration> removed in removedItems)
        {
            if (removed.Value is PrimaryCommandConfiguration configuration &&
                provider.Get(configuration) is IWidgetComponentViewModel viewModel)
            {
                await mediator.PublishAsync(
                    new Removed<IWidgetComponentViewModel>(viewModel),
                    removed.Key.ParentId == Guid.Empty ? nameof(PrimaryWidgetViewModel) : removed.Key.ParentId,
                    cancellationToken);

                cache.Remove(removed.Key);
            }
        }
    }
}
