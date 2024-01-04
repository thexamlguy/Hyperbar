using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace Hyperbar.Desktop;

public sealed partial class CommandView : Page
{
    public CommandView() => InitializeComponent();

    protected override void OnKeyDown(KeyRoutedEventArgs e)
    {
        base.OnKeyDown(e);
    }
}
