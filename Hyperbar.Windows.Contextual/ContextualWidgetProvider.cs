using Hyperbar.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Widget.Contextual;

public class ContextualWidgetProvider :
    IWidgetProvider
{
    public void Create(HostBuilderContext comtext, IServiceCollection services) => services
            .AddConfiguration<ContextualWidgetConfiguration>(comtext.Configuration.GetSection(nameof(ContextualWidgetConfiguration)))
            .AddWidgetTemplate<ContextualWidgetViewModel>();
}