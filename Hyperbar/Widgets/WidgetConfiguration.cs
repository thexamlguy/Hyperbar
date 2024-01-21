namespace Hyperbar;

public interface IWidgetConfiguration
{
    Guid Id { get; set; }

    string? Name { get; set; }
    
    string? Description { get; set; }
}

public abstract class WidgetConfiguration :
    IWidgetConfiguration
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public string? Description { get; set; }
}
