using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.UI.Windows;

public class ContentControlHandler(IViewModelContentBinder viewModelContentBinder) :
    INavigationHandler<ContentControl>
{
    public Task Handle(Navigate<ContentControl> args,
        CancellationToken cancellationToken)
    {
        if (args.Target is ContentControl contentControl)
        {
            contentControl.Content = args.View;
            contentControl.DataContext = args.ViewModel;
        }

        return Task.CompletedTask;
    }
}
