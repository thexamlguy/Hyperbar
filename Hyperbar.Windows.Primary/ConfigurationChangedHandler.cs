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
        foreach (KeyValuePair<Guid, IWidgetComponentViewModel> cached in cache)
        {
            if (configuration.FirstOrDefault(x => x.Id == cached.Key) == null)
            {
                await mediator.PublishAsync(new Removed<IWidgetComponentViewModel>(cached.Value),
                    cancellationToken);

                cache.Remove(cached.Key);
            }
        }

        foreach (PrimaryCommandConfiguration item in configuration)
        {
            
            //if (!cache.ContainsKey(item.Id))
            //{
            //    factory.CreateAsync(item);
            //}
            //else
            //{

            //}
        }    }
}
