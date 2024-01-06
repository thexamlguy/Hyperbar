using Hyperbar.Lifecycles;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Windows.Contextual;

public class ContextualCommandWidgetBuilder : 
    ICommandWidgetBuilder
{
    public void Create(IServiceCollection services)
    {
        services
            .AddWritableConfiguration<ContextualCommandWidgetConfiguration>()
            .AddCommandTemplate<ContextualCommandWidgetViewModel, ContextualCommandWidgetView>();
    }
}
