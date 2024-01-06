using Hyperbar.Windows.Win32;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace Hyperbar.Windows.Controls;

internal class DesktopFlyoutHost : Window
{
    private readonly DesktopFlyoutPresenter presenter;
    private DesktopFlyoutPlacement placement;
    private Popup? popup;

    public DesktopFlyoutHost(DesktopFlyoutPresenter presenter)
    {
        Border root = new();
        root.Loaded += OnLoaded;

        this.presenter = presenter;
        presenter.SizeChanged += OnChildSizeChanged;

        Content = root;

        this.SetOpacity(0);
        this.SetStyle(WindowStyle.SysMenu | WindowStyle.Visible);
        this.SetTopMost(true);
        this.SetIsShownInSwitchers(false);
    }

    internal void UpdatePlacement(DesktopFlyoutPlacement placement)
    {
        this.placement = placement;

        // Not ready
        if (popup is null)
        {
            return;
        }

        //  presenter.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

        double height = presenter.DesiredSize.Height;
        double width = presenter.DesiredSize.Width;

        switch (placement)
        {
            case DesktopFlyoutPlacement.Left:
                this.Snap(WindowPlacement.Left, 0, 0);
                break;

            case DesktopFlyoutPlacement.Top:
                this.Snap(WindowPlacement.Top, width, height);
                break;

            case DesktopFlyoutPlacement.Right:
                this.Snap(WindowPlacement.Right, 0, 0);
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
        SizeChangedEventArgs args) => UpdatePlacement(this.placement);

    private void OnLoaded(object sender,
        RoutedEventArgs args)
    {
        popup = new Popup
        {
            Child = presenter,
            XamlRoot = Content.XamlRoot,
            ShouldConstrainToRootBounds = false,
            IsOpen = true,
        };

        UpdatePlacement(placement);
    }
}