namespace Hyperbar.Widget.Primary.Windows;

public class KeyAcceleratorCommandConfiguration :
    PrimaryCommandConfiguration
{
    public required int Key { get; set; }

    public int[]? Modifiers { get; set; }
}
