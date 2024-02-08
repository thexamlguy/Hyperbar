namespace Hyperbar;

public interface IViewModelTemplateDescriptor
{
    object Key { get; set; }

    Type TemplateType { get; set; }

    Type ViewModelType { get; set; }
}