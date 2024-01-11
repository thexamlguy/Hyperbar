namespace Hyperbar.Windows.Primary;

public class KeyAcceleratorCommandConfiguration :
    PrimaryCommandConfiguration
{
    public required int Key { get; set; }

    public int[]? Modifiers { get; set; }
}
