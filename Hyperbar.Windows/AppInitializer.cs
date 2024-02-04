using Hyperbar.Controls.Windows;
using Hyperbar.Widget;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Windows;

public class AppInitializer([FromKeyedServices(nameof(ApplicationBarViewModel))] ApplicationBarView view,
    [FromKeyedServices(nameof(ApplicationBarViewModel))] ApplicationBarViewModel viewModel,
    DesktopApplicationBar desktopFlyout, 
    AppConfiguration configuration) :
    IInitializer
{
    public Task InitializeAsync()
    {
        view.DataContext = viewModel;

        desktopFlyout.Placement = configuration.Placement;
        desktopFlyout.Content = view;

        return Task.CompletedTask;
    }
}