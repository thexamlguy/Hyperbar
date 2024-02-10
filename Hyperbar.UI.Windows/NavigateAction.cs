using Microsoft.UI.Xaml;
using Microsoft.Xaml.Interactivity;

namespace Hyperbar.UI.Windows;

public sealed class NavigateAction :
    DependencyObject,
    IAction
{
    public static readonly DependencyProperty PathProperty = 
        DependencyProperty.Register(nameof(Path),
            typeof(string), typeof(NavigateAction), 
                new PropertyMetadata(null));

    public string Path
    {
        get => (string)GetValue(PathProperty);
        set => SetValue(PathProperty, value);
    }

    public object Execute(object sender, object parameter)
    {
        if (sender is FrameworkElement frameworkElement)
        {
            if (frameworkElement.DataContext is IObservableViewModel observableViewModel)
            {
                observableViewModel.Publisher.PublishAsync(new Navigate(Path))
                    .GetAwaiter().GetResult();
            }
        }

        return true;
    }
}
