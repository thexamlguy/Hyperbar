using Hyperbar.Desktop.Controls;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Hyperbar.Desktop;

public class AppInitializer([FromKeyedServices("Commands")] CommandView view,
    [FromKeyedServices("Commands")] CommandViewModel viewModel,
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
