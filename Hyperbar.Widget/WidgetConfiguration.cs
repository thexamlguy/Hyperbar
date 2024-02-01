using System.Text.Json.Serialization;

namespace Hyperbar.Widget;
public class WidgetConfiguration 
{
    public string? Description { get; set; }

    public string? Name { get; set; }

    [JsonInclude]
    internal Guid Id { get; set; } = Guid.NewGuid();

    [JsonInclude]
    internal bool IsEnabled { get; set; }
}

public class WidgetConfiguration<TConfiguration>;
