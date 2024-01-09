using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace Hyperbar.Windows;

public sealed partial class WidgetView :
    UserControl,
    IWidgetView
{
    public WidgetView()
    {
        InitializeComponent();
    }

    protected override void OnKeyDown(KeyRoutedEventArgs e)
    {
        base.OnKeyDown(e);
    }
}