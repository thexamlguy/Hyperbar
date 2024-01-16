using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Hyperbar.Windows.UI;
using Windows.Foundation;
using WinUIEx;
using WindowStyle = Hyperbar.Windows.Interop.WindowStyle;
using ExtendedWindowStyle = Hyperbar.Windows.Interop.ExtendedWindowStyle;

namespace Hyperbar.Windows.Controls;

internal class DesktopFlyoutHost : Window
{
    private readonly DesktopFlyoutPresenter presenter;
    private bool loaded;
    private DesktopFlyoutPlacement placement;

    public DesktopFlyoutHost(DesktopFlyoutPresenter presenter)
    {
        SystemBackdrop = new TransparentTintBackdrop();

        this.SetOpacity(0);
        this.Snap(WindowPlacement.Top, 0, 0);

        this.SetStyle(WindowStyle.SysMenu | WindowStyle.Visible);
        this.SetStyle(ExtendedWindowStyle.NoActivate);

        this.SetTopMost(true);
        this.SetIsAvailableInSwitchers(false);

        Border root = new();
        root.Loaded += OnLoaded;
        Content = root;

        this.presenter = presenter;
    }

    internal void UpdatePlacement(DesktopFlyoutPlacement placement)
    {
        this.placement = placement;

        // Not ready
        if (!loaded)
        {
            return;
        }

        presenter.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

        double height = presenter.DesiredSize.Height;
        double width = presenter.DesiredSize.Width;

        switch (placement)
        {
            case DesktopFlyoutPlacement.Left:
                this.Snap(WindowPlacement.Left, height, width);
                break;

            case DesktopFlyoutPlacement.Top:
                this.Snap(WindowPlacement.Top, width, height);
                break;

            case DesktopFlyoutPlacement.Right:
                this.Snap(WindowPlacement.Right, height, width);
                break;

            case DesktopFlyoutPlacement.Bottom:
                this.Snap(WindowPlacement.Bottom, width, height);
                break;

            default:
                break;
        }

        presenter.TemplateSettings.SetValue(DesktopFlyoutPresenterTemplateSettings.HeightProperty, height);
        presenter.TemplateSettings.SetValue(DesktopFlyoutPresenterTemplateSettings.WidthProperty, width);

        presenter.TemplateSettings.SetValue(DesktopFlyoutPresenterTemplateSettings.NegativeHeightProperty, -height);
        presenter.TemplateSettings.SetValue(DesktopFlyoutPresenterTemplateSettings.NegativeWidthProperty, -width);

        presenter.UpdatePlacementState(placement);
    }

    private void OnChildSizeChanged(object sender,
        SizeChangedEventArgs args) => UpdatePlacement(placement);

    private void OnLoaded(object sender,
        RoutedEventArgs args)
    {
        this.SetOpacity(255);

        if (Content is Border border)
        {
            border.Child = presenter;

            double height = presenter.DesiredSize.Height;
            double width = presenter.DesiredSize.Width;

            presenter.SizeChanged += OnChildSizeChanged;
        }

        loaded = true;
        UpdatePlacement(placement);
    }
}