using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetProvider :
    IWidgetProvider
{
    public void Create(HostBuilderContext comtext, IServiceCollection services) =>
            services.AddConfiguration<PrimaryWidgetConfiguration>()
                .AddCache<Guid, IWidgetComponentViewModel>()
                .AddTransient<IFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?>, WidgetComponentViewModelFactory>()
                .AddTransient<IViewModelEnumerator<IWidgetComponentViewModel>, WidgetComponentViewModelEnumerator>()
                .AddWidgetTemplate<PrimaryWidgetViewModel>()
                .AddHandler<PrimaryWidgetConfigurationHandler>();

}