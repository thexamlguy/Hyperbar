namespace Hyperbar.Windows.Primary;

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
                items.Add(new KeyValuePair<(Guid ParentId, Guid Id), PrimaryCommandConfiguration>((currentParentId, configuration.Id), configuration));
                if (configuration.Commands is not null && configuration.Commands.Count > 0)
                {
                    stack.Push((configuration.Id, configuration.Commands));
                }
            }
        }

        foreach (KeyValuePair<(Guid ParentId, Guid Id), PrimaryCommandConfiguration> moved in
            items.ExceptBy(cache.Select(x => new { x.Value.Order, x.Value.Id }), x => new { x.Value.Order, x.Value.Id }))
        {
            if (moved.Value is PrimaryCommandConfiguration configuration)
            {
                if (provider.Get(configuration) is IWidgetComponentViewModel viewModel)
                {
                    await mediator.PublishAsync(new Moved<IWidgetComponentViewModel>(configuration.Order, viewModel),
                       moved.Key.ParentId == Guid.Empty ? nameof(PrimaryWidgetViewModel) : moved.Key.ParentId,
                            cancellationToken);

                    cache.Remove(moved.Key);
                    cache.Add(moved.Key, moved.Value);
                }
            }

     


            //   // cache.Add(added.Key, added.Value);
            //}
        }

        foreach (KeyValuePair<(Guid ParentId, Guid Id), PrimaryCommandConfiguration> added in
            items.ExceptBy(cache.Select(x => x.Key.Id), x => x.Key.Id))
        {
            if (added.Value is PrimaryCommandConfiguration configuration)
            {
                if (factory.Create(configuration) is IWidgetComponentViewModel viewModel)
                {
                    await mediator.PublishAsync(new Inserted<IWidgetComponentViewModel>(configuration.Order, viewModel),
                       added.Key.ParentId == Guid.Empty ? nameof(PrimaryWidgetViewModel) : added.Key.ParentId,
                            cancellationToken);
                }

                cache.Add(added.Key, added.Value);
            }
        }

        foreach (KeyValuePair<(Guid ParentId, Guid Id), PrimaryCommandConfiguration> removed in
            cache.ExceptBy(items.Select(x => x.Key.Id), x => x.Key.Id))
        {
            if (removed.Value is PrimaryCommandConfiguration configuration)
            {
                if (provider.Get(configuration) is IWidgetComponentViewModel viewModel)
                {
                    await mediator.PublishAsync(new Removed<IWidgetComponentViewModel>(viewModel),
                        removed.Key.ParentId == Guid.Empty ? nameof(PrimaryWidgetViewModel) : removed.Key.ParentId,
                            cancellationToken);

                    cache.Remove(removed.Key);
                }
            }
        }
    }
}
