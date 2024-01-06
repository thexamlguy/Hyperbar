using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget.Contextual;

public class ContextualWidgetBuilder : 
    IWidgetBuilder
{
    public void Create(IServiceCollection services) => services
            .AddConfiguration<ContextualWidgetConfiguration>()
            .AddWidgetTemplate<ContextualWidgetViewModel>();
}
