using Microsoft.UI.Xaml;

namespace Hyperbar.Windows;

public partial class SettingsView :
    Window
{
    public SettingsView() => 
        InitializeComponent();

    protected SettingsViewModel ViewModel =>
        (SettingsViewModel)(Content as FrameworkElement)!.DataContext;
}
