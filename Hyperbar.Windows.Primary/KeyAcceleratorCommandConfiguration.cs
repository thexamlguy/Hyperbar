namespace Hyperbar.Windows.Primary;

public class KeyAcceleratorCommandConfiguration :
    IPrimaryCommandConfiguration
{
    public string? Icon { get; set; }

    public string? Key { get; set; }

    public string[]? Modifiers { get; set; }
}