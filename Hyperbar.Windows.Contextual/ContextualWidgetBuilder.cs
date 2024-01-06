using Hyperbar.Lifecycles;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Extensions.Contextual;

public class ContextualWidgetBuilder : 
    IWidgetBuilder
{
    public void Create(IServiceCollection services)
    {
        services
            .AddConfiguration<ContextualWidgetConfiguration>()
            .AddWidgetTemplate<ContextualWidgetViewModel>();
    }
}
