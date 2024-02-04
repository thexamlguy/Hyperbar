using Hyperbar.Widget;
using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.Windows;

public partial class SecondaryView : 
    UserControl
{
    public SecondaryView() =>
        InitializeComponent();

    protected SecondaryViewModel ViewModel =>
        (SecondaryViewModel)DataContext;
}
