using Microsoft.UI.Xaml.Controls;
using Hyperbar.UI.Windows;

namespace Hyperbar.Widget.MediaController.Windows;

public partial class MediaControllerView : 
    UserControl
{
    public MediaControllerView() =>
        this.InitializeComponent(ref _contentLoaded);

    protected MediaControllerViewModel ViewModel => 
        (MediaControllerViewModel)DataContext;
}
