using Hyperbar.Widgets;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Windows.Primary;

public class PrimaryWidget :
    IWidget
{
    public IWidgetBuilder Create() =>
        WidgetBuilder<PrimaryWidgetConfiguration>.Configure(args =>
        {
            args.Id = Guid.Parse("cfdfe07c-d9d6-4174-ae41-988ca24d2e10");
            args.Name = "Primary commands";
        }).ConfigureServices(services =>
        {
            services.AddCache<(Guid ParentId, Guid Id), PrimaryCommandConfiguration>()
                .AddCache<Guid, IWidgetComponentViewModel>()
                .AddTransient<IProvider<PrimaryCommandConfiguration, IWidgetComponentViewModel?>, WidgetComponentProvider>()
                .AddTransient<IFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?>, WidgetComponentFactory>()
                .AddTransient<IEnumerator<IWidgetComponentViewModel>, WidgetComponentEnumerationHandler>()
                .AddWidgetTemplate<PrimaryWidgetViewModel>()
                .AddHandler<PrimaryWidgetConfigurationHandler>();
        });
}