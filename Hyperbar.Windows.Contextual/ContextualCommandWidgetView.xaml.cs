using Hyperbar.Windows.Win32;
using Microsoft.UI.Xaml.Controls;
using Windows.System;

namespace Hyperbar.Windows.Contextual;

public sealed partial class ContextualCommandWidgetView : Page
{
    public ContextualCommandWidgetView() => InitializeComponent();

    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        KeyInterop.Type(VirtualKey.L, VirtualKey.LeftWindows, VirtualKey.Control);
    }
}
