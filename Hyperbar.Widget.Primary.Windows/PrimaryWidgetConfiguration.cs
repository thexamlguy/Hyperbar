using Hyperbar.Widget;

namespace Hyperbar.Widget.Primary.Windows;

public class PrimaryWidgetConfiguration : 
    WidgetConfiguration
{
    public List<PrimaryCommandConfiguration> Commands { get; set; } = [];
}