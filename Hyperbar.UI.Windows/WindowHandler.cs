using Microsoft.UI.Xaml;

namespace Hyperbar.UI.Windows;

public class WindowHandler(IViewModelContentBinder viewModelContentBinder) :
    INavigationHandler<Window>
{
    public Task Handle(Navigate<Window> args,
        CancellationToken cancellationToken)
    {
        if (args.Target is Window window)
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

                viewModelContentBinder.Bind(content, window);
                window.Closed += HandleClosed;
                content.DataContext = args.ViewModel;
            }

            window.Activate();
        }

        return Task.CompletedTask;
    }
}
