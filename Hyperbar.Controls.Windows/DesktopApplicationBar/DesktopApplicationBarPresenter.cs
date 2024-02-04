using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;

namespace Hyperbar.Controls.Windows;

public class DesktopApplicationBarPresenter :
    ContentControl
{
    public static readonly DependencyProperty TemplateSettingsProperty =
        DependencyProperty.Register(nameof(TemplateSettings),
            typeof(DesktopApplicationBarPresenterTemplateSettings), typeof(DesktopApplicationBarPresenter),
            new PropertyMetadata(null));

    internal new DesktopApplicationBar? Parent;

    public DesktopApplicationBarPresenter()
    {
        DefaultStyleKey = typeof(DesktopApplicationBarPresenter);
        TemplateSettings = new DesktopApplicationBarPresenterTemplateSettings();
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

    public DesktopApplicationBarPresenterTemplateSettings TemplateSettings
    {
        get => (DesktopApplicationBarPresenterTemplateSettings)GetValue(TemplateSettingsProperty);
        set => SetValue(TemplateSettingsProperty, value);
    }

    internal void UpdatePlacementState(DesktopApplicationBarPlacemenet placement) => VisualStateManager.GoToState(this, $"{placement}Placement", true);
}