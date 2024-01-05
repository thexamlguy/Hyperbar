namespace Hyperbar.Templates;

public record DataTemplateDescriptor :
    IDataTemplateDescriptor
{
    public required Type DataType { get; set; }

    public required Type TemplateType { get; set; }

    public required object Key { get; set; }
}

