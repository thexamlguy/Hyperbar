namespace Hyperbar;

public record ViewModelTemplateDescriptor :
    IViewModelTemplateDescriptor
{
    public required Type ViewModelType { get; set; }

    public required Type TemplateType { get; set; }

    public required object Key { get; set; }
}