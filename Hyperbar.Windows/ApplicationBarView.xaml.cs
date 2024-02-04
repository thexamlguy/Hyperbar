using Hyperbar.Widget;
using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.Windows;

public partial class ApplicationBarView :
    UserControl
{
    public ApplicationBarView() => 
        InitializeComponent();
    protected ApplicationBarViewModel ViewModel =>
        (ApplicationBarViewModel)DataContext;
}