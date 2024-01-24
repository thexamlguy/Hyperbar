using Hyperbar.Widget;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Windows.Primary;

public class PrimaryWidget :
    IWidget
{
    public IWidgetBuilder Create() =>
        WidgetBuilder<PrimaryWidgetConfiguration>.Configure(args =>
        {
            args.Name = "Primary commands";
            args.Commands =
            [
                new KeyAcceleratorCommandConfiguration
                {
                    Id = Guid.NewGuid(),
                    Order = 0,
                    Text = "Test",
                    Icon = "dd",
                    Key = 1
                }
            ];
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