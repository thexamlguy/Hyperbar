namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetConfigurationHandler(IMediator mediator,
    PrimaryWidgetConfiguration configuration,
    IFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?> factory,
    ICache<Guid, IWidgetComponentViewModel> cache) :
    INotificationHandler<ConfigurationChanged<PrimaryWidgetConfiguration>>
{
    public async ValueTask Handle(ConfigurationChanged<PrimaryWidgetConfiguration> notification,
        CancellationToken cancellationToken)
    {
        HashSet<Guid> configurationIds = new(configuration.SelectMany(item => new[] { item }
            .Concat(item.Commands).Select(x => x.Id)));

        foreach (KeyValuePair<Guid, IWidgetComponentViewModel> item in cache.Where(x => !configurationIds.Contains(x.Key)))
        {
            await mediator.PublishAsync(new Removed<IWidgetComponentViewModel>(item.Value),
                cancellationToken);
            cache.Remove(item.Key);
        }

        foreach (PrimaryCommandConfiguration item in configuration)
        {
            if (!cache.ContainsKey(item.Id))
            {
                if (factory.Create(item) is IWidgetComponentViewModel value)
                {
                    await mediator.PublishAsync(Inserted<IWidgetComponentViewModel>
                            .For<PrimaryWidgetViewModel>(item.Order, value),
                                cancellationToken);
                }
            }
        }
    }
}
