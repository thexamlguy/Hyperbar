using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.Windows;

public partial class SettingsButtonView : 
    UserControl
{
    public SettingsButtonView() => 
        InitializeComponent();

    protected SettingsButtonViewModel ViewModel =>
        (SettingsButtonViewModel)DataContext;
}
