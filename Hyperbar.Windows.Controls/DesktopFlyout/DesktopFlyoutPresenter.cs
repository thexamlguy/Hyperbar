using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;

namespace Hyperbar.Windows.Controls;

public class DesktopFlyoutPresenter :
    ContentControl
{
    public static readonly DependencyProperty TemplateSettingsProperty =
        DependencyProperty.Register(nameof(TemplateSettings),
            typeof(DesktopFlyoutPresenterTemplateSettings), typeof(DesktopFlyoutPresenter),
            new PropertyMetadata(null));

    internal new DesktopFlyout Parent;

    public DesktopFlyoutPresenter()
    {
        DefaultStyleKey = typeof(DesktopFlyoutPresenter);
        TemplateSettings = new DesktopFlyoutPresenterTemplateSettings();
    }

    protected override void OnApplyTemplate()
    {
        SetBinding(ContentProperty, new Binding
        {
            Source = Parent,
            Mode = BindingMode.TwoWay,
            Path = new PropertyPath(nameof(Parent.Content)),
        });
    }

    public DesktopFlyoutPresenterTemplateSettings TemplateSettings
    {
        get => (DesktopFlyoutPresenterTemplateSettings)GetValue(TemplateSettingsProperty);
        set => SetValue(TemplateSettingsProperty, value);
    }

    internal void UpdatePlacementState(DesktopFlyoutPlacement placement) => VisualStateManager.GoToState(this, $"{placement}Placement", true);
}