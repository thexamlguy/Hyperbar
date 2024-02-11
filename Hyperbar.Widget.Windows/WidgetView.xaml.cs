using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.Widget.Windows;

public sealed partial class WidgetView :
    UserControl,
    IWidgetView
{
    public WidgetView() => InitializeComponent();
}