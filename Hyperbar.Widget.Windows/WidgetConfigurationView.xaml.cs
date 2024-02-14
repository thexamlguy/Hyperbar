using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.Widget.Windows;

public partial class WidgetConfigurationView : UserControl
{
    public WidgetConfigurationView() => 
        InitializeComponent();

    protected WidgetConfigurationViewModel ViewModel =>
        (WidgetConfigurationViewModel)DataContext;
}
