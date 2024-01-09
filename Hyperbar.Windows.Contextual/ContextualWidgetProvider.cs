using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Widget.Contextual;

public class ContextualWidgetProvider :
    IWidgetProvider
{
    public void Create(HostBuilderContext comtext, IServiceCollection services) => services
            .AddWidgetTemplate<ContextualWidgetViewModel>();
}