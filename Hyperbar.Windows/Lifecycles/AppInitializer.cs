using Hyperbar.Controls.Windows;
using Hyperbar.Widget;
using Hyperbar.Widget.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Windows;

public class AppInitializer([FromKeyedServices(nameof(WidgetViewModel))] WidgetBarView view,
    [FromKeyedServices(nameof(WidgetViewModel))] WidgetViewModel viewModel,
    DesktopBar desktopFlyout, 
    AppConfiguration configuration) :
    IInitialization
{
    public Task InitializeAsync()
    {
        view.DataContext = viewModel;

        desktopFlyout.Placement = configuration.Placement;
        desktopFlyout.Content = view;

        return Task.CompletedTask;
    }
}