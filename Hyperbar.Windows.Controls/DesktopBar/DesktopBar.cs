using Microsoft.UI.Xaml;

namespace Hyperbar.Windows.Controls;

public class DesktopBar :
    DependencyObject
{
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content),
            typeof(object), typeof(DesktopBar),
            new PropertyMetadata(null));

    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Placement),
            typeof(DesktopBarPlacemenet), typeof(DesktopBar),
            new PropertyMetadata(DesktopBarPlacemenet.Left, OnPlacementPropertyChanged));

    private readonly DesktopBarHost host;
    private readonly DesktopBarPresenter presenter;

    public DesktopBar()
    {
        presenter = new DesktopBarPresenter
        {
            Parent = this
        };

        host = new DesktopBarHost(presenter);
        host.Activate();
    }

    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public DesktopBarPlacemenet Placement
    {
        get => (DesktopBarPlacemenet)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    private static void OnPlacementPropertyChanged(DependencyObject dependencyObject,
        DependencyPropertyChangedEventArgs args)
    {
        if (dependencyObject is DesktopBar sender)
        {
            sender.OnPlacementPropertyChanged();
        }
    }

    private void OnPlacementPropertyChanged() => UpdatePlacement();

    private void UpdatePlacement() => host.UpdatePlacement(Placement);
}