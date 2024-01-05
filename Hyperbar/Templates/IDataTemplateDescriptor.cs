namespace Hyperbar.Templates;

public interface IDataTemplateDescriptor
{
    Type DataType { get; set; }
    object Key { get; set; }
    Type TemplateType { get; set; }
}

