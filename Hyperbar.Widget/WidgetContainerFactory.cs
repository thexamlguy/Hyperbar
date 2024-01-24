using Microsoft.Extensions.DependencyInjection;
namespace Hyperbar.Widget;

public class WidgetContainerFactory(IServiceFactory factory) :
    IFactory<IWidgetHost, WidgetContainerViewModel?>
{
    public WidgetContainerViewModel? Create(IWidgetHost value)
    {
        if (value.Services.GetServices<IWidgetViewModel>() is 
                IEnumerable<IWidgetViewModel> viewModels)
        {
            return factory.Create<WidgetContainerViewModel>(value.Configuration.Id);
        }

        return default;
    }
}
