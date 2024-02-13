using Microsoft.UI.Xaml;
using Microsoft.Xaml.Interactivity;

namespace Hyperbar.UI.Windows;

public sealed class NavigateAction :
    DependencyObject,
    IAction
{
    public static readonly DependencyProperty NameProperty = 
        DependencyProperty.Register(nameof(Name),
            typeof(string), typeof(NavigateAction), 
                new PropertyMetadata(null));

    public static readonly DependencyProperty TargetNameProperty =
        DependencyProperty.Register(nameof(Name),
            typeof(string), typeof(NavigateAction),
                new PropertyMetadata(null));
    public string Name
    {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    public string TargetName
    {
        get => (string)GetValue(TargetNameProperty);
        set => SetValue(TargetNameProperty, value);
    }

    public object Execute(object sender,
        object parameter)
    {
        if (sender is FrameworkElement frameworkElement)
        {
            if (frameworkElement.DataContext is IObservableViewModel observableViewModel)
            {
                observableViewModel.Publisher.PublishAsync(new Navigate(Name, TargetName ?? null))
                    .GetAwaiter().GetResult();
            }
        }

        return true;
    }
}
