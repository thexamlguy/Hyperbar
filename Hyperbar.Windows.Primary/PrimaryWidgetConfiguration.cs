namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetConfiguration :
    List<PrimaryCommandConfiguration>
{
    public static PrimaryWidgetConfiguration Defaults => new()
    {
        new KeyAcceleratorCommandConfiguration { Icon = "\uE720", Key = 91, Modifiers = [] }
    };
}