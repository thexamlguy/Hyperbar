namespace Hyperbar.Windows.Primary;

public class WidgetComponentMappinglFactory :
    MappingFactory<PrimaryWidgetConfiguration, IEnumerable<IWidgetComponentViewModel>>
{
    private readonly PrimaryWidgetConfiguration configuration;
    private readonly IServiceFactory service;

    public WidgetComponentMappinglFactory(PrimaryWidgetConfiguration configuration, 
        IServiceFactory service)
    {
        this.configuration = configuration;
        this.service = service;
    }

    public IEnumerable<IWidgetComponentViewModel> Create()
    {
        foreach (IPrimaryCommandConfiguration item in configuration)
        {
            
        }

        return Enumerable.Empty<IWidgetComponentViewModel>();
    }
}
