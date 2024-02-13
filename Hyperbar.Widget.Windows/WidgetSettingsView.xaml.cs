using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.Widget.Windows;

public partial class WidgetSettingsView : UserControl
{
    public WidgetSettingsView() => 
        InitializeComponent();

    protected WidgetSettingsViewModel ViewModel =>
        (WidgetSettingsViewModel)DataContext;
}
