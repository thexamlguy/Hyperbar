namespace Hyperbar;

public interface IWidget
{
    Guid Id { get; set; }

    string? Name { get; set; }

    string? Description { get; set; }
}
