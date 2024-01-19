namespace Hyperbar;

public class Widget :
    IWidget
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public string? Description { get; set; }
}
