using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.UI.Windows;

public class FrameHandler :
    INavigationHandler<Frame>
{
    public Task Handle(Navigate<Frame> args,
        CancellationToken cancellationToken = default)
    {

        return Task.CompletedTask;
    }
}

public class WindowHandler :
    INavigationHandler<Window>
{
    public Task Handle(Navigate<Window> args,
        CancellationToken cancellationToken)
    {
        if (args.Template is Window window)
        {
            if (window.Content is FrameworkElement content)
            {
                void HandleClosed(object sender, WindowEventArgs args)
                {
                    window.Closed -= HandleClosed;
                    if (content.DataContext is IObservableViewModel observableViewModel)
                    {
                        observableViewModel.Dispose();
                    }
                }

                window.Closed += HandleClosed;
                content.DataContext = args.Content;
            }

            window.Activate();
        }

        return Task.CompletedTask;
    }
}
