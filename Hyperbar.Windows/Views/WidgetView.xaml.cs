using Hyperbar.Widget;
using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.Windows;

public sealed partial class WidgetView :
    UserControl,
    IWidgetView
{
    public WidgetView() => InitializeComponent();
}