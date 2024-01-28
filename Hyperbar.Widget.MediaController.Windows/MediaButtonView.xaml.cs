using Microsoft.UI.Xaml.Controls;
using Hyperbar.UI.Windows;

namespace Hyperbar.Widget.MediaController.Windows;

public sealed partial class MediaButtonView : 
    UserControl
{
    public MediaButtonView() =>
        this.InitializeComponent(ref _contentLoaded);
}
