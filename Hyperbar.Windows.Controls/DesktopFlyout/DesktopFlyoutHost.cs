using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Hyperbar.Windows.UI;
using Windows.Foundation;
using WinUIEx;
using WindowStyle = Hyperbar.Windows.Interop.WindowStyle;
using System.Diagnostics;

namespace Hyperbar.Windows.Controls;

internal class DesktopFlyoutHost : Window
{
    private readonly DesktopFlyoutPresenter presenter;
    private bool loaded;
    private DesktopFlyoutPlacement placement;

    public DesktopFlyoutHost(DesktopFlyoutPresenter presenter)
    {
        Border root = new();
        root.Loaded += OnLoaded;
        Content = root;

        this.presenter = presenter;

        this.SetOpacity(0);
        this.SetStyle(WindowStyle.SysMenu | WindowStyle.Visible);
        this.SetTopMost(true);
        this.SetIsShownInSwitchers2(false);
    }

    internal async void UpdatePlacement(DesktopFlyoutPlacement placement)
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

        Debug.WriteLine(height);
        Debug.WriteLine(width);

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

        await Task.Delay(TimeSpan.FromSeconds(4));

        presenter.UpdatePlacementState(placement);
    }

    private void OnChildSizeChanged(object sender,
        SizeChangedEventArgs args) => UpdatePlacement(placement);

    private void OnLoaded(object sender,
        RoutedEventArgs args)
    {
        SystemBackdrop = new TransparentTintBackdrop();
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