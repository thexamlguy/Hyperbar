using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetConfigurationBuilder :
    IWidgetBuilder
{
    public void Create(IServiceCollection services) =>
        WidgetBuilder.Config(services, config =>
        {
            config.Id = Guid.Parse("cfdfe07c-d9d6-4174-ae41-988ca24d2e10");
            config.Name = "Primary commands";

            services.AddConfiguration<PrimaryWidgetConfiguration>()
                    .AddCache<(Guid ParentId, Guid Id), PrimaryCommandConfiguration>()
                    .AddCache<Guid, IWidgetComponentViewModel>()
                    .AddTransient<IProvider<PrimaryCommandConfiguration, IWidgetComponentViewModel?>, WidgetComponentViewModelProvider>()
                    .AddTransient<IFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?>, WidgetComponentViewModelFactory>()
                    .AddTransient<IViewModelEnumerator<IWidgetComponentViewModel>, WidgetComponentViewModelEnumerator>()
                    .AddWidgetTemplate<PrimaryWidgetViewModel>()
                    .AddHandler<PrimaryWidgetConfigurationHandler>();
        });
}