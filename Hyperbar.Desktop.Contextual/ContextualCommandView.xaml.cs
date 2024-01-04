using Hyperbar.Desktop.Win32;
using Microsoft.UI.Xaml.Controls;
using Windows.System;

namespace Hyperbar.Desktop.Contextual;

public sealed partial class ContextualCommandView : Page
{
    public ContextualCommandView() => InitializeComponent();

    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        KeyIntrop.Type((VirtualKey)186, VirtualKey.LeftWindows);
    }
}
