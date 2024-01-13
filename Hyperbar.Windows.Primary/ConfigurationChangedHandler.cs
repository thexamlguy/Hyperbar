namespace Hyperbar.Windows.Primary;

public class ConfigurationChangedHandler(IMediator mediator,
    PrimaryWidgetConfiguration configuration,
    IViewModelFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?> factory,
    IViewModelCache<Guid, IWidgetComponentViewModel> cache) :
    INotificationHandler<ConfigurationChanged<PrimaryWidgetConfiguration>>
{
    public async ValueTask Handle(ConfigurationChanged<PrimaryWidgetConfiguration> notification,
        CancellationToken cancellationToken)
    {
        foreach (KeyValuePair<Guid, IWidgetComponentViewModel> item in cache)
        {
            if (configuration.FirstOrDefault(x => x.Id == item.Key) == null)
            {
                await mediator.PublishAsync(new Removed<IWidgetComponentViewModel>(item.Value),
                    cancellationToken);

                cache.Remove(item.Key);
            }
        }

        foreach (PrimaryCommandConfiguration item in configuration)
        {
            if (!cache.ContainsKey(item.Id))
            {
                if (factory.Create(item) is IWidgetComponentViewModel value)
                {
                    await mediator.PublishAsync(new Created<IWidgetComponentViewModel>(value),
                        cancellationToken);
                }
            }
        }
    }
}
