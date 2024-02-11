using Hyperbar.UI.Windows;
using Microsoft.UI;
using Microsoft.UI.Xaml;

namespace Hyperbar.Windows;

[NavigationTarget("Settings")]
public partial class SettingsView :
    Window
{
    public SettingsView()
    {
        InitializeComponent();

        this.TitleBarConfiguration(args => 
        {
            args.ExtendsContentIntoTitleBar = true;
            args.ButtonBackgroundColor = Colors.Transparent;
            args.ButtonInactiveBackgroundColor = Colors.Transparent;
        });
    }

    protected SettingsViewModel ViewModel =>
        (SettingsViewModel)(Content as FrameworkElement)!.DataContext;
}
