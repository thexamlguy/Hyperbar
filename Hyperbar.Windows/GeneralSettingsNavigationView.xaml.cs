using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.Windows;

public partial class GeneralSettingsNavigationView : 
    NavigationViewItem
{
    public GeneralSettingsNavigationView() => 
        InitializeComponent();

    protected GeneralSettingsNavigationViewModel ViewModel =>
        (GeneralSettingsNavigationViewModel)DataContext;
}
