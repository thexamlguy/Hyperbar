using Microsoft.UI.Xaml;

namespace Hyperbar.Desktop.Controls;

public class DesktopFlyout :
    DependencyObject
{
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content),
            typeof(object), typeof(DesktopFlyout),
            new PropertyMetadata(null));

    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Placement),
            typeof(DesktopFlyoutPlacement), typeof(DesktopFlyout),
            new PropertyMetadata(DesktopFlyoutPlacement.Left, OnPlacementPropertyChanged));

    private readonly DesktopFlyoutHost host;
    private readonly DesktopFlyoutPresenter presenter;

    public DesktopFlyout()
    {
        presenter = new DesktopFlyoutPresenter
        {
            Parent = this
        };

        host = new DesktopFlyoutHost(presenter);
        host.Activate();
    }

    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public DesktopFlyoutPlacement Placement
    {
        get => (DesktopFlyoutPlacement)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    private static void OnPlacementPropertyChanged(DependencyObject dependencyObject,
        DependencyPropertyChangedEventArgs args)
    {
        if (dependencyObject is DesktopFlyout sender)
        {
            sender.OnPlacementPropertyChanged();
        }
    }

    private void OnPlacementPropertyChanged() => UpdatePlacement();

    private void UpdatePlacement() => host.UpdatePlacement(Placement);
}
