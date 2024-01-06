namespace Hyperbar.Templates;

public interface IContentTemplateDescriptor
{
    Type ContentType { get; set; }

    object Key { get; set; }

    Type TemplateType { get; set; }
}

