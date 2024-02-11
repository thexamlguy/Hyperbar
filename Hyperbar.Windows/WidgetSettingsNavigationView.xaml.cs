using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.Windows;

public partial class WidgetSettingsNavigationView : 
    NavigationViewItem
{
    public WidgetSettingsNavigationView() => 
        InitializeComponent();

    protected WidgetSettingsNavigationViewModel ViewModel =>
        (WidgetSettingsNavigationViewModel)DataContext;
}
