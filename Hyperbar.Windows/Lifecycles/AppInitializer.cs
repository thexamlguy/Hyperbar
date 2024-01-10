using Hyperbar.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Windows;

public class AppInitializer([FromKeyedServices(nameof(WidgetBarViewModel))] WidgetBarView view,
    [FromKeyedServices(nameof(WidgetBarViewModel))] WidgetBarViewModel viewModel,
    DesktopFlyout desktopFlyout) :
    IInitializer
{
    public Task InitializeAsync()
    {
        view.DataContext = viewModel;

        desktopFlyout.Placement = DesktopFlyoutPlacement.Top;
        desktopFlyout.Content = view;

        return Task.CompletedTask;
    }
}