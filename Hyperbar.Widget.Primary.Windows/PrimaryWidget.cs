using Hyperbar.Widget;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget.Primary.Windows;

public class PrimaryWidget :
    IWidget
{
    public IWidgetBuilder Create() =>
        WidgetBuilder.Create()
            .UseConfiguration<PrimaryWidgetConfiguration>(args =>
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
                    .AddWidgetTemplate<PrimaryWidgetViewModel>()
                    .AddHandler<WidgetComponentViewModelEnumerator>()
                    .AddHandler<PrimaryWidgetConfigurationHandler>();
            });
}