using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.Widget.Windows;

public partial class WidgetNavigationView : 
    NavigationViewItem
{
    public WidgetNavigationView() => 
        InitializeComponent();

    protected WidgetNavigationViewModel ViewModel =>
        (WidgetNavigationViewModel)DataContext;
}
