using Microsoft.UI.Xaml;

namespace Hyperbar.UI.Windows;

public class WindowHandler :
    INavigationHandler<Window>
{
    public Task Handle(Navigate<Window> args,
        CancellationToken cancellationToken)
    {
        if (args.Template is Window window)
        {
            if (window.Content is FrameworkElement frameworkElement)
            {
                frameworkElement.DataContext = args.Content;
            }

            window.Activate();
        }

        return Task.CompletedTask;
    }
}
