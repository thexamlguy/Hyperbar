using Microsoft.UI.Xaml.Controls;
using Hyperbar.UI.Windows;

namespace Hyperbar.Widget.MediaController.Windows;

public partial class MediaControllerWidgetView :
    UserControl
{
    public MediaControllerWidgetView() => 
        this.InitializeComponent(ref _contentLoaded);

    protected MediaControllerWidgetViewModel ViewModel => 
        (MediaControllerWidgetViewModel)DataContext;
}