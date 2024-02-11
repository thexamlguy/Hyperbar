namespace Hyperbar;

public record ViewModelTemplate :
    IViewModelTemplate
{
    public required Type ViewModelType { get; set; }

    public required Type ViewType { get; set; }

    public required object Key { get; set; }
}