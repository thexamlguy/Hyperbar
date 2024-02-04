using Microsoft.UI.Xaml;
using Windows.Foundation;
using Microsoft.UI.Xaml.Media;
using Hyperbar.Interop.Windows;
using Hyperbar.UI.Windows;

namespace Hyperbar.Controls.Windows;

internal class DesktopApplicationBarHost : Window
{
    private readonly DesktopApplicationBarPresenter presenter;
    private readonly WindowSnapping windowSnapping;
    private DesktopApplicationBarPlacemenet placement;

    public DesktopApplicationBarHost(DesktopApplicationBarPresenter presenter)
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

    internal void UpdatePlacement(DesktopApplicationBarPlacemenet placement)
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
            case DesktopApplicationBarPlacemenet.Left:
                windowSnapping.Snap(WindowSnappingPlacement.Left, (int)size);
                break;

            case DesktopApplicationBarPlacemenet.Top:
                windowSnapping.Snap(WindowSnappingPlacement.Top, (int)size);
                break;

            case DesktopApplicationBarPlacemenet.Right:
                windowSnapping.Snap(WindowSnappingPlacement.Right, (int)size);
                break;

            case DesktopApplicationBarPlacemenet.Bottom:
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