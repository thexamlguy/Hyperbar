using Microsoft.UI.Xaml.Controls;
using Hyperbar.UI.Windows;

namespace Hyperbar.Widget.MediaController.Windows;

public sealed partial class MediaControllerView : 
    UserControl
{
    public MediaControllerView() =>
        this.InitializeComponent(ref _contentLoaded);
}
