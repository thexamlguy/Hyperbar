using Microsoft.UI.Xaml;

namespace Hyperbar.Controls.Windows;

public class DesktopApplicationBar :
    DependencyObject
{
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content),
            typeof(object), typeof(DesktopApplicationBar),
            new PropertyMetadata(null));

    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Placement),
            typeof(DesktopApplicationBarPlacemenet), typeof(DesktopApplicationBar),
            new PropertyMetadata(DesktopApplicationBarPlacemenet.Left, OnPlacementPropertyChanged));

    private readonly DesktopApplicationBarHost host;
    private readonly DesktopApplicationBarPresenter presenter;

    public DesktopApplicationBar()
    {
        presenter = new DesktopApplicationBarPresenter
        {
            Parent = this
        };

        host = new DesktopApplicationBarHost(presenter);
        host.Activate();
    }

    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public DesktopApplicationBarPlacemenet Placement
    {
        get => (DesktopApplicationBarPlacemenet)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    private static void OnPlacementPropertyChanged(DependencyObject dependencyObject,
        DependencyPropertyChangedEventArgs args)
    {
        if (dependencyObject is DesktopApplicationBar sender)
        {
            sender.OnPlacementPropertyChanged();
        }
    }

    private void OnPlacementPropertyChanged() => UpdatePlacement();

    private void UpdatePlacement() => host.UpdatePlacement(Placement);
}