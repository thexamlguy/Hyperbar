namespace Hyperbar.Widget;

public class WidgetContainerFactory(IServiceFactory factory) :
    IFactory<IWidgetHost, WidgetContainerViewModel?>
{
    public WidgetContainerViewModel? Create(IWidgetHost value)
    {
        return factory.Create<WidgetContainerViewModel>(value.Configuration.Id);
    }
}
