namespace Hyperbar;

public interface IDataTemplateDescriptor
{
    Type DataType { get; set; }
    object Key { get; set; }
    Type TemplateType { get; set; }
}

