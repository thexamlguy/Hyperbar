using Hyperbar.Desktop.Controls;
using Hyperbar.Lifecycles;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Hyperbar.Desktop;

public class AppInitializer([FromKeyedServices(nameof(CommandViewModel))] CommandView view,
    [FromKeyedServices(nameof(CommandViewModel))] CommandViewModel viewModel,
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
