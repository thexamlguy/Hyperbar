﻿namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetConfiguration :
    List<PrimaryCommandConfiguration>
{
    public static PrimaryWidgetConfiguration Defaults => new()
    {
        new KeyAcceleratorCommandConfiguration { Id = Guid.NewGuid(), Icon = "\uE720", Text = "Test", Key = 91, Modifiers = [] }
    };
}