namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetViewModelFactory :
    IViewModelFactory<PrimaryWidgetConfiguration, IEnumerable<IWidgetComponentViewModel>>
{
    private readonly PrimaryWidgetConfiguration configuration;
    private readonly IServiceFactory service;

    public PrimaryWidgetViewModelFactory(PrimaryWidgetConfiguration configuration, 
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
