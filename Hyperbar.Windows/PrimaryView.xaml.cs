using Hyperbar.Widget;
using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.Windows;

public partial class PrimaryView : 
    UserControl
{
    public PrimaryView() => 
        InitializeComponent();

    protected PrimaryViewModel ViewModel =>
        (PrimaryViewModel)DataContext;
}
