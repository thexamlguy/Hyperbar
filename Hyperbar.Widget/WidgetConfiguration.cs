using System.Text.Json.Serialization;

namespace Hyperbar.Widget;
public class WidgetConfiguration 
{
    public string? Description { get; set; }

    [JsonInclude]
    internal Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    [JsonInclude]
    internal bool IsAvailable { get; set; }
}

public class WidgetConfiguration<TConfiguration>;
