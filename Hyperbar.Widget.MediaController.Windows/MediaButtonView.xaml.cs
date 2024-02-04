using Microsoft.UI.Xaml.Controls;
using Hyperbar.UI.Windows;

namespace Hyperbar.Widget.MediaController.Windows;

public partial class MediaButtonView : 
    UserControl
{
    public MediaButtonView() =>
        this.InitializeComponent(ref _contentLoaded);

    protected IMediaButtonViewModel ViewModel => 
        (IMediaButtonViewModel)DataContext;
}
