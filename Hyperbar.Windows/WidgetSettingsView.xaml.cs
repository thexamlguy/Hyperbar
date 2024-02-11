using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.Windows;

public partial class WidgetSettingsView : UserControl
{
    public WidgetSettingsView() => 
        InitializeComponent();

    protected WidgetSettingsViewModel ViewModel =>
        (WidgetSettingsViewModel)DataContext;
}
