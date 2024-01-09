using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetProvider :
    IWidgetProvider
{
    public void Create(HostBuilderContext comtext, IServiceCollection services) => 
            services.AddConfiguration<PrimaryWidgetConfiguration>()
                .AddHandler<WidgetComponentMapping>()
                .AddHandler<PrimaryWidgetConfigurationChangedHandler>()
                .AddWidgetTemplate<PrimaryWidgetViewModel>();
}