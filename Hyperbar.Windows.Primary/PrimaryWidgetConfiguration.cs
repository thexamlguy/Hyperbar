namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetConfiguration :
    List<KeyAcceleratorCommandConfiguration>
{
    public static PrimaryWidgetConfiguration Defaults => new()
    {
        new KeyAcceleratorCommandConfiguration { Icon = "Test", Key = "Test", Modifiers = ["Test", "Test"] }
    };
}