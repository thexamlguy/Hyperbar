using Microsoft.UI.Xaml.Controls;
using Hyperbar.UI.Windows;

namespace Hyperbar.Widget.MediaController.Windows;

public sealed partial class MediaControllerWidgetView :
    UserControl
{
    public MediaControllerWidgetView() => 
        this.InitializeComponent(ref _contentLoaded);
}