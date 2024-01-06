namespace Hyperbar.Templates;

public record ContentTemplateDescriptor :
    IContentTemplateDescriptor
{
    public required Type ContentType { get; set; }

    public required Type TemplateType { get; set; }

    public required object Key { get; set; }
}

