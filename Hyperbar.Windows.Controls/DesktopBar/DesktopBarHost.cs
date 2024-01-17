using Microsoft.UI.Xaml;
using Hyperbar.Windows.UI;
using Windows.Foundation;
using WindowStyle = Hyperbar.Windows.Interop.WindowStyle;
using ExtendedWindowStyle = Hyperbar.Windows.Interop.ExtendedWindowStyle;
using Hyperbar.Windows.Interop;
using Microsoft.UI.Xaml.Media;

namespace Hyperbar.Windows.Controls;

internal class DesktopBarHost : Window
{
    private readonly DesktopBarPresenter presenter;
    private readonly WindowSnapping windowSnapping;
    private DesktopBarPlacemenet placement;

    public DesktopBarHost(DesktopBarPresenter presenter)
    {
        this.SetOpacity(0);
        this.SetStyle(WindowStyle.SysMenu | WindowStyle.Visible);
        this.SetStyle(ExtendedWindowStyle.NoActivate);
        this.MoveAndResize(0, 0, 0, 0);
        this.SetTopMost(true);
        this.SetIsAvailableInSwitchers(false);

        SystemBackdrop = new MicaBackdrop();
        windowSnapping = WindowSnapping.Create(this.GetHandle());

        this.presenter = presenter;
        presenter.Loaded += OnLoaded;
        Content = presenter;

        Closed += OnClosed;
    }

    internal void UpdatePlacement(DesktopBarPlacemenet placement)
    {
        this.placement = placement;
        UpdatePlacement();
    }

    internal void UpdatePlacement()
    {
        presenter.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        double size = Math.Min(presenter.DesiredSize.Height, presenter.DesiredSize.Width);

        switch (placement)
        {
            case DesktopBarPlacemenet.Left:
                windowSnapping.Snap(WindowSnappingPlacement.Left, (int)size);
                break;

            case DesktopBarPlacemenet.Top:
                windowSnapping.Snap(WindowSnappingPlacement.Top, (int)size);
                break;

            case DesktopBarPlacemenet.Right:
                windowSnapping.Snap(WindowSnappingPlacement.Right, (int)size);
                break;

            case DesktopBarPlacemenet.Bottom:
                windowSnapping.Snap(WindowSnappingPlacement.Bottom, (int)size);
                break;

            default:
                break;
        }

        presenter.UpdatePlacementState(placement);
    }

    private void OnClosed(object sender, WindowEventArgs args)
    {
        windowSnapping.Dispose();
    }
    private void OnLoaded(object sender,
        RoutedEventArgs args)
    {
        UpdatePlacement();
        this.SetOpacity(255);
    }
}