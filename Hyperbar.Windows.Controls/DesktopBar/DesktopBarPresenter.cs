using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;

namespace Hyperbar.Windows.Controls;

public class DesktopBarPresenter :
    ContentControl
{
    public static readonly DependencyProperty TemplateSettingsProperty =
        DependencyProperty.Register(nameof(TemplateSettings),
            typeof(DesktopBarPresenterTemplateSettings), typeof(DesktopBarPresenter),
            new PropertyMetadata(null));

    internal new DesktopBar? Parent;

    public DesktopBarPresenter()
    {
        DefaultStyleKey = typeof(DesktopBarPresenter);
        TemplateSettings = new DesktopBarPresenterTemplateSettings();
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

    public DesktopBarPresenterTemplateSettings TemplateSettings
    {
        get => (DesktopBarPresenterTemplateSettings)GetValue(TemplateSettingsProperty);
        set => SetValue(TemplateSettingsProperty, value);
    }

    internal void UpdatePlacementState(DesktopBarPlacemenet placement) => VisualStateManager.GoToState(this, $"{placement}Placement", true);
}