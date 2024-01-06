using Hyperbar.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget.Contextual;

public class ContextualWidgetProvider :
    IWidgetProvider
{
    public void Create(IServiceCollection services) => services
            .AddConfiguration<ContextualWidgetConfiguration>()
            .AddWidgetTemplate<ContextualWidgetViewModel>();
}