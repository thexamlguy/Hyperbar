using Microsoft.UI.Xaml.Controls;
using Hyperbar.UI.Windows;

namespace Hyperbar.Widget.MediaController.Windows;

public partial class MediaInformationView : 
    UserControl
{
    public MediaInformationView() =>
        this.InitializeComponent(ref _contentLoaded);

    protected MediaInformationViewModel ViewModel => 
        (MediaInformationViewModel)DataContext;
}
